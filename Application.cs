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

        public Application()
        {
            Instance = this;

            mainForm = new MainForm();

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

            HotKey hotKey = new HotKey(Keys.H, HotKeyModifiers.Control);
            mainForm.RegisterHotKey(hotKey);
            mainForm.HotKeyPressed += new EventHandler<HotKeyEventArgs>(HotKeyManager_HotKeyPressed);
        }

        void HotKeyManager_HotKeyPressed(object sender, HotKeyEventArgs e)
        {
            Exit();
        }

        void HideShowMainForm(object sender = null, EventArgs e = null)
        {
            mainForm.Visible = !mainForm.Visible;
        }

        public void Exit(object sender = null, EventArgs e = null)
        {
            mainForm.ForceClose();

            // Hide tray icon, otherwise it will remain shown until user mouses over it
            trayIcon.Visible = false;
            System.Windows.Forms.Application.Exit();
        }
    }
}
