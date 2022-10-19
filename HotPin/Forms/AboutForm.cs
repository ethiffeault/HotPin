using System.Windows.Forms;

namespace HotPin
{
    public partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();
            textBox.SelectionStart = 0;
            pictureBox.BackgroundImage = Application.Resources.HotPin;
        }
    }
}
