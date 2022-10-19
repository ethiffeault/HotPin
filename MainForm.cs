using HotPin.Properties;
using System.IO;
using System.Windows.Forms;

namespace HotPin
{
    public partial class MainForm : HotKeyForm
    {
        public const string Title = "HotPin";

        private bool forceClose = false;
        private bool debugClose = false;
        private bool projectDirty = false;

        public MainForm()
        {
            InitializeComponent();

            Text = Title;

            Icon = Resources.HotPin;

            Application.Instance.ProjectLoaded += ProjectLoaded;
            Application.Instance.ProjectSaving += ProjectSaving;
            Application.Instance.ProjectSaved += ProjectSaved;

            projectControl.OnProjectChanged += ProjectChanged;
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

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (System.Diagnostics.Debugger.IsAttached)
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

        private void MainMenuFileSaveClick(object sender, System.EventArgs e)
        {
            if (projectDirty)
            {
                Application.Instance.SaveProject();
            }
        }

        private void MainMenuFileExit(object sender, System.EventArgs e)
        {
            Application.Instance.Exit();
        }

        private void MainMenuFileLoad(object sender, System.EventArgs e)
        {
            Application.Instance.LoadProject();
        }

        private void DebugLogLevelMenuItem_DropDownOpened(object sender, System.EventArgs e)
        {
            DebugLogLevelInfoMenuItem.Checked = Log.Setting.LogLevel == LogLevel.Info;
            DebugLogLevelWarningMenuItem.Checked = Log.Setting.LogLevel == LogLevel.Warning;
            DebugLogLevelErrorMenuItem.Checked = Log.Setting.LogLevel == LogLevel.Error;
        }

        private void DebugLogLevelInfoMenuItem_Click(object sender, System.EventArgs e)
        {
            Log.Setting.LogLevel = LogLevel.Info;
            Settings.Save();
        }

        private void DebugLogLevelWarningMenuItem_Click(object sender, System.EventArgs e)
        {
            Log.Setting.LogLevel = LogLevel.Warning;
            Settings.Save();
        }

        private void DebugLogLevelErrorMenuItem_Click(object sender, System.EventArgs e)
        {
            Log.Setting.LogLevel = LogLevel.Error;
            Settings.Save();
        }

        private void openLogToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            Log.Open();
        }
    }
}
