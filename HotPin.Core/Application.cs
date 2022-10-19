using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace HotPin
{
    public class ApplicationSettings : Settings.Entry
    {
        public bool ShowWelcomeScreen = true;
    }

    public class Application : ApplicationContext
    {
        public static Application Instance { get; } = new Application();
        public static ApplicationSettings Settings { get => HotPin.Settings.Get<ApplicationSettings>(); }
        public const string Name = "HotPin";
        public static string Version { get; } = "0.0.2";

        public HotKeyForm Form { get; private set; }
        public Project Project { get; private set; }
        public Executor Executor { get; private set; }
        public bool DebugMode { get; } = System.Diagnostics.Debugger.IsAttached;
        public List<Assembly> CommandAssemblies { get; private set; } = new List<Assembly>();

        public Action ProjectSaving;
        public Action ProjectSaved;
        public Action ProjectLoaded;
        public Action ProjectClosed;
        public Action ForceClose;

        private NotifyIcon trayIcon;
        private bool closing = false;

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
        }

        public Application()
        {
        }

        public void Init(HotKeyForm form)
        {
            Log.DefaultContext = Application.Name;
            Log.Info("Starting HotPin!");

            Form = form;
            Executor = new Executor(form);

            Form.Show();
            if (DebugMode == false && Settings.ShowWelcomeScreen == false)
                Form.Visible = false;

            // Initialize Tray Icon
            trayIcon = new NotifyIcon()
            {
                Icon = Core.Resources.HotPinIcon,
                ContextMenu = new ContextMenu(new MenuItem[] {
                new MenuItem("Hide/Show", HideShowMainForm),
                new MenuItem("Exit", Exit)
            }),
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

        public static string Path { get => new FileInfo(System.Reflection.Assembly.GetEntryAssembly().Location).DirectoryName; }

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
    }
}
