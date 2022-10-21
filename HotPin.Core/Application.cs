using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace HotPin
{
    public class ApplicationSettings : Settings.Entry
    {
        public bool ShowWelcomeScreen = true;
        public bool DebugMode = false;
        public bool ShowCrashButton = false;
    }

    public class Application : ApplicationContext
    {
        public static Application Instance { get; } = new Application();
        public static ApplicationSettings Settings { get => HotPin.Settings.Get<ApplicationSettings>(); }
        public const string Name = "HotPin";
        public static string Version { get; } = ReadVersion();
        public static string Path { get; } = new FileInfo(System.Reflection.Assembly.GetEntryAssembly().Location).DirectoryName;
        public static string Licence { get; } = System.IO.Path.Combine(Path, "LICENSE.txt");
        public const string ProjectHome = "https://github.com/ethiffeault/HotPin";
        public const string ProjectRelease = "https://github.com/ethiffeault/HotPin/releases";
        public const string ProjectOwner = "ethiffeault";
        public const string ProjectName = "HotPin";

        public HotKeyForm Form { get; private set; }
        public Project Project { get; private set; }
        public Executor Executor { get; private set; }
        public static bool DebugMode { get; private set; } = System.Diagnostics.Debugger.IsAttached;
        public static bool DisableCrashReportDebug = false; // set to true to debug crash reporting
        public List<Assembly> CommandAssemblies { get; private set; } = new List<Assembly>();

        public Action ProjectSaving;
        public Action ProjectSaved;
        public Action ProjectLoaded;
        public Action ProjectClosed;
        public Action ForceClose;

        private NotifyIcon trayIcon;
        private bool closing = false;
        private ContextMenuStrip trayIconMenu;
        private ToolStripMenuItem trayIconItemHideShow;
        private ToolStripMenuItem trayIconItemExit;

        // expose resources
        public static class Resources
        {
            public static readonly Icon HotPinIcon = Core.Resources.HotPinIcon;
            public static readonly Image HotPin = Core.Resources.HotPin;
            public static readonly Image HotPinGrey = Core.Resources.HotPinGrey;
            public static readonly Image Save = Core.Resources.Save;
            public static readonly Image Load = Core.Resources.Load;
            public static readonly Image Exit = Core.Resources.Exit;
            public static readonly Image Configuration = Core.Resources.Configuration;
            public static readonly Image Open = Core.Resources.Open;
            public static readonly Image Reload = Core.Resources.Reload;
            public static readonly Image Log = Core.Resources.Log;
            public static readonly Image Info = Core.Resources.Info;
            public static readonly Image Warning = Core.Resources.Warning;
            public static readonly Image Error = Core.Resources.Error;
            public static readonly Image Level = Core.Resources.Level;
            public static readonly Image Plus = Core.Resources.Plus;
            public static readonly Image Minus = Core.Resources.Minus;
            public static readonly Image Playlist = Core.Resources.Playlist;
            public static readonly Image Chat = Core.Resources.Chat;
            public static readonly Image Problem = Core.Resources.Problem;
        }

        public Application()
        {
        }

        public void Init(HotKeyForm form)
        {
            DebugMode = System.Diagnostics.Debugger.IsAttached || Settings.DebugMode;
            CrashHandler.Init(DebugMode && !DisableCrashReportDebug);
            CrashHandler.Crashed += Crashed;

            Log.DefaultContext = Application.Name;
            Log.Info("Starting HotPin!");

            Form = form;
            Executor = new Executor(form);

            Form.Show();
            if (DebugMode == false && Settings.ShowWelcomeScreen == false)
                Form.Visible = false;

            // Initialize Tray Icon
            trayIconMenu = new ContextMenuStrip();
            trayIconMenu.Opening += TrayIconMenuOpening;

            trayIconItemHideShow = new ToolStripMenuItem("Hide/Show");
            trayIconItemHideShow.Image = Core.Resources.HotPinGrey;
            trayIconItemHideShow.Click += HideShowMainForm;
            trayIconMenu.Items.Add(trayIconItemHideShow);

            trayIconItemExit = new ToolStripMenuItem("Exit");
            trayIconItemExit.Image = Core.Resources.Exit;
            trayIconItemExit.Click += Exit;
            trayIconMenu.Items.Add(trayIconItemExit);

            trayIcon = new NotifyIcon()
            {
                Icon = Core.Resources.HotPinIcon,
                ContextMenuStrip = trayIconMenu,
                Visible = true
            };

            trayIcon.MouseDoubleClick += HideShowMainForm;

            LoadPlugins();
            LoadProject();

            if (Settings.ShowWelcomeScreen)
            {
                Settings.ShowWelcomeScreen = false;
                HotPin.Settings.Save();
                string message =
@"Welcome to HotPin!

Next time you will start HotPin, it will be minimized in the task bar tray.
To always have tray icon visible:
    1. Right-click on the Taskbar
    2. Taskbar Settings
    3. Taskbar corner overflow
    4. Turn HotPin On

Have Fun!";
                MessageBoxEx.Show(Form, message, "HotPin!");
            }
        }

        private void TrayIconMenuOpening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (Form.Visible)
            {
                trayIconItemHideShow.Text = "Hide";
                trayIconItemHideShow.Image = Core.Resources.HotPinGrey;
            }
            else
            {
                trayIconItemHideShow.Text = "Show";
                trayIconItemHideShow.Image = Core.Resources.HotPin;
            }
        }

        private void LoadPlugins()
        {
            string[] commandFiles = Directory.GetFiles(Path, "HotPin.Commands.*");

            foreach (string commandFile in commandFiles)
            {
                if (commandFile.EndsWith(".dll"))
                {
                    Log.Info($"Loading commands from {new FileInfo(commandFile).Name}");
                    CommandAssemblies.Add(Assembly.LoadFile(commandFile));
                }
            }
        }

        private void HideShowMainForm(object sender = null, EventArgs e = null)
        {
            Form.Visible = !Form.Visible;
            if (Form.Visible)
            {
                // bring to front if needed
                Form.Activate();
                Form.Focus();
            }
        }

        public void Exit(object sender = null, EventArgs e = null)
        {
            if (!closing)
            {
                closing = true;

                ProjectClosed?.Invoke();
                ForceClose?.Invoke();

                // Hide tray icon, otherwise it will remain shown until user mouses over it
                trayIcon.Visible = false;
                Log.Info("Closing HotPin");
                HotPin.Settings.Save();
                System.Windows.Forms.Application.Exit();
            }
        }

        public void SaveProject()
        {
            ProjectSaving?.Invoke();
            Project.Save(Project);
            ProjectSaved?.Invoke();
        }

        public void LoadProject()
        {
            Project = Project.Load();
            ProjectLoaded?.Invoke();
        }

        private static string ReadVersion()
        {
            Assembly assembly = typeof(Application).Assembly;
            string resourceName = assembly.GetManifestResourceNames().Single(str => str.EndsWith("version.txt"));
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                string version = reader.ReadToEnd();
                return version.Trim(' ', '\r', '\n');
            }
        }

        private void Crashed(Exception e)
        {
            DialogResult result = MessageBoxEx.Show("HotPin crashed, please report it.", "HotPin", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);

            if (result == DialogResult.OK)
            {
                string body = $"<Enter description here>\n\nHotPin v{Version}\n<b>{e.Message}</b>\n\n```{e.StackTrace}```";
                string url = GitHub.GetCreateIssueUrl(ProjectOwner, ProjectName, label: GitHub.Label.Bug, body: body);
                Utils.StartProcess(url);
                trayIcon.Visible = false;
            }
        }
    }
}
