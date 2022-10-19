using System.Windows.Forms;

namespace HotPin
{
    public partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();
            pictureBox.BackgroundImage = Application.Resources.HotPin;
            textBox.Text = $"HotPin\r\nversion {Application.Version}";
            textBox.SelectionStart = 0;
        }
    }
}
