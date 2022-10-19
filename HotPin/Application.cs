using HotPin.Properties;
using System;
using System.Windows.Forms;

namespace HotPin
{
    internal class Application : ApplicationContext
    {
        public static Application Instance { get; private set; }

        public MainForm Form { get; private set; }
        public Project Project { get; private set; }

        public Action ProjectSaving;
        public Action ProjectSaved;
        public Action ProjectLoaded;
        public Action ProjectClosed;

        private NotifyIcon trayIcon;
        public Executor Executor { get; private set; }
        public bool closing = false;

        public Application()
        {
            Log.Info("Starting HotPin!", "HotPin");

            Instance = this;

            Form = new MainForm();
            Executor = new Executor(Form);

            Form.Show();
            if (!System.Diagnostics.Debugger.IsAttached)
                Form.Visible = false;

            // Initialize Tray Icon
            trayIcon = new NotifyIcon()
            {
                Icon = Resources.HotPin,
                ContextMenu = new ContextMenu(new MenuItem[] {
                new MenuItem("Hide/Show", HideShowMainForm),
                new MenuItem("Exit", Exit)
            }),
                Visible = true
            };

            trayIcon.MouseDoubleClick += HideShowMainForm;

            LoadProject();
        }

        void HideShowMainForm(object sender = null, EventArgs e = null)
        {
            Form.Visible = !Form.Visible;
        }

        public void Exit(object sender = null, EventArgs e = null)
        {
            if (!closing)
            {
                closing = true;

                ProjectClosed?.Invoke();
                Form.ForceClose();

                // Hide tray icon, otherwise it will remain shown until user mouses over it
                trayIcon.Visible = false;
                System.Windows.Forms.Application.Exit();

                Log.Info("Closing HotPin", "HotPin");
                Settings.Save();
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
