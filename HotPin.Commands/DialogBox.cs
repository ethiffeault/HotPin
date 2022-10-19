using System.Drawing;
using System.Threading.Tasks;

namespace HotPin.Commands
{
    public class DialogBox : Command
    {
        static readonly Image DialogBoxImage = Resources.DialogBox;

        [Parameter(Description = "dialog title")]
        public string Title { get; set; } = Application.Name;

        [Parameter(Description = "dialog message")]
        public string Message { get; set; } = $"Welcome to {Application.Name}!";

        public override Image Image => DialogBoxImage;

        protected override async Task OnExecute()
        {
            MessageBoxEx.Show(Message, Title);
        }
    }
}
