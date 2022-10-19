using System.Drawing;
using System.Threading.Tasks;

namespace HotPin.Commands
{
    public class DialogBox : Command
    {
        static readonly Image DialogBoxImage = Resources.DialogBox;

        public string Title { get; set; } = "HotPin";
        public string Message { get; set; } = "Welcome to HotPin!";
        public override Image Image => DialogBoxImage;

        public override async Task Run()
        {
            MessageBoxEx.Show(Message, Title);
        }

        public override string ToString()
        {
            return $"DialogBox : {Name}";
        }
    }
}
