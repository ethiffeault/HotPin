using HotPin.Properties;
using System.Windows.Forms;

namespace HotPin
{
    public partial class MainForm : HotKeyForm
    {
        private bool forceClose = false;
        private bool debugClose = false;

        public MainForm()
        {
            InitializeComponent();
            Icon = Resources.HotPin;
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

    }
}
