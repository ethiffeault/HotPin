using System;
using System.IO;
using System.Windows.Forms;

namespace HotPin
{
    public partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();
            pictureBox.BackgroundImage = Application.Resources.HotPin;
            textBox.Text = $"HotPin{Environment.NewLine}version {Application.Version}";
            textBox.SelectionStart = 0;
        }

        private void buttonLicence_Click(object sender, EventArgs e)
        {
            Utils.StartProcess( Application.Licence );
        }
    }
}
