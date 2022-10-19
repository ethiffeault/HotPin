using HotPin.Properties;
using System;
using System.Windows.Forms;

namespace HotPin
{
    internal class Application : ApplicationContext
    {
        public static Application Instance { get; private set; }

        private NotifyIcon trayIcon;
        private MainForm mainForm;
        public Project Project { get; private set; }

        public Action ProjectSaving;
        public Action ProjectSaved;
        public Action ProjectLoaded;

        private Executor executor;
        public bool closed = false;

        public Application()
        {
            Log.Info("Starting HotPin!", "HotPin");

            Instance = this;

            mainForm = new MainForm();
            executor = new Executor(mainForm);

            mainForm.Show();
            if (!System.Diagnostics.Debugger.IsAttached)
                mainForm.Visible = false;

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
            mainForm.Visible = !mainForm.Visible;
        }

        public void Exit(object sender = null, EventArgs e = null)
        {
            if (!closed)
            {
                mainForm.ForceClose();

                // Hide tray icon, otherwise it will remain shown until user mouses over it
                trayIcon.Visible = false;
                System.Windows.Forms.Application.Exit();

                Log.Info("Closing HotPin", "HotPin");
                Settings.Save();
                closed = true;
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
