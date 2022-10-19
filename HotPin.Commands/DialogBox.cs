using System.Drawing;
using System.Threading.Tasks;

namespace HotPin.Commands
{
    public class DialogBox : Command
    {
        static readonly Image DialogBoxImage = Resources.DialogBox;

        public string Title { get; set; } = Application.Name;
        public string Message { get; set; } = $"Welcome to {Application.Name}!";
        public override Image Image => DialogBoxImage;

        protected override async Task OnExecute()
        {
            MessageBoxEx.Show(Message, Title);
        }
    }
}
