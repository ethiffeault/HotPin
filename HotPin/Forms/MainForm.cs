using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HotPin
{
    public partial class MainForm : HotKeyForm
    {
        public const string Title = Application.Name;

        private bool forceClose = false;
        private bool debugClose = false;
        private bool projectDirty = false;
        private string latestVersion = null;

        public MainForm()
        {
            InitializeComponent();

            Text = Title;

            Icon = Application.Resources.HotPinIcon;

            Application.Instance.ProjectLoaded += ProjectLoaded;
            Application.Instance.ProjectSaving += ProjectSaving;
            Application.Instance.ProjectSaved += ProjectSaved;
            Application.Instance.ForceClose += ForceClose;

            projectControl.OnProjectChanged += ProjectChanged;

            // images
            menuItemFileSave.Image = Application.Resources.Save;
            menuItemFileLoad.Image = Application.Resources.Load;
            menuItemFileExit.Image = Application.Resources.Exit;

            menuItemToolsConfiguration.Image = Application.Resources.Configuration;
            menuItemToolsConfigurationOpen.Image = Application.Resources.Open;
            menuItemToolsConfigurationReload.Image = Application.Resources.Reload;

            menuItemDebugLog.Image = Application.Resources.Log;
            menuItemDebugLogLevel.Image = Application.Resources.Level;
            menuItemDebugLogOpen.Image = Application.Resources.Open;
            menuItemDebugLogLevelInfo.Image = Application.Resources.Info;
            menuItemDebugLogLevelWarning.Image = Application.Resources.Warning;
            menuItemDebugLogLevelError.Image = Application.Resources.Error;

            menuItemHelpAbout.Image = Application.Resources.HotPin;

            menuItemRunning.Text = "";
            menuItemRunning.Image = Application.Resources.HotPinGrey;

            _ = CheckNewVersion();
        }

        private void ProjectLoaded()
        {
            projectControl.ReadProject();
            projectDirty = false;
        }

        private void ProjectSaving()
        {
            projectControl.WriteProject();
        }

        private void ProjectSaved()
        {
            projectControl.ReadProject();
            Text = Title;
            projectDirty = false;
        }

        private void MainFormClosing(object sender, FormClosingEventArgs e)
        {
            if (Application.Instance.DebugMode)
            {
                debugClose = true;
                Application.Instance.Exit();
            }
            else
            {
                if (!forceClose)
                {
                    e.Cancel = true;
                    Visible = false;
                }
            }
        }

        public void ForceClose()
        {
            if (debugClose)
                return;
            forceClose = true;
            this.Close();
        }

        public void ProjectChanged()
        {
            projectDirty = true;
            Text = Title + "*";
        }

        private void MenuItemFileSaveClick(object sender, System.EventArgs e)
        {
            if (projectDirty)
            {
                Application.Instance.SaveProject();
            }
        }

        private void MenuItemFileExit(object sender, System.EventArgs e)
        {
            Application.Instance.Exit();
        }

        private void MenuItemFileLoad(object sender, System.EventArgs e)
        {
            Application.Instance.LoadProject();
        }

        private void MenuItemDebugLogLevelDropDownOpened(object sender, System.EventArgs e)
        {
            menuItemDebugLogLevelInfo.Checked = Log.Setting.LogLevel == LogLevel.Info;
            menuItemDebugLogLevelWarning.Checked = Log.Setting.LogLevel == LogLevel.Warning;
            menuItemDebugLogLevelError.Checked = Log.Setting.LogLevel == LogLevel.Error;
        }

        private void MenuItemDebugLogLevelInfoClick(object sender, System.EventArgs e)
        {
            Log.Setting.LogLevel = LogLevel.Info;
            Settings.Save();
        }

        private void MenuItemDebugLogLevelWarningClick(object sender, System.EventArgs e)
        {
            Log.Setting.LogLevel = LogLevel.Warning;
            Settings.Save();
        }

        private void MenuItemDebugLogLevelErrorClick(object sender, System.EventArgs e)
        {
            Log.Setting.LogLevel = LogLevel.Error;
            Settings.Save();
        }

        private void MenuItemDebugLogOpenClick(object sender, System.EventArgs e)
        {
            Log.View();
        }

        private void MenuItemToolsConfigurationOpenClick(object sender, System.EventArgs e)
        {
            Settings.Edit();
        }

        private void MenuItemToolsConfigurationReloadClick(object sender, System.EventArgs e)
        {
            Settings.Load();
        }

        private void MenuItemHelpAboutClick(object sender, System.EventArgs e)
        {
            AboutForm aboutForm = new AboutForm();
            aboutForm.ShowDialog(this);
        }

        private void TimerTick(object sender, System.EventArgs e)
        {
            if (Application.Instance.Executor.IsRunnig)
            {
                menuItemRunning.Image = Application.Resources.HotPin;
                progressBar.Visible = true;
            }
            else
            {
                menuItemRunning.Image = Application.Resources.HotPinGrey;
                progressBar.Visible = false;
            }
        }

        private async Task CheckNewVersion()
        {
            latestVersion = await GitHub.GetLatestVersion(Application.ProjectOwner, Application.ProjectName);

            if (latestVersion != null && Application.Version != latestVersion)
            {
                menuItemRunning.Text = "New Version Available!";
            }
        }

        private void MenuItemRunningClick(object sender, System.EventArgs e)
        {
            if (latestVersion != null && Application.Version != latestVersion)
                Utils.StartProcess($"{Application.ProjectRelease}/tag/{latestVersion}");
            else
                Utils.StartProcess(Application.ProjectHome);
        }
    }
}
