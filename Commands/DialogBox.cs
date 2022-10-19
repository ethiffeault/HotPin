using System.Threading.Tasks;

namespace HotPin.Commands
{
    public class DialogBox : Command
    {
        public override async Task Run()
        {
            MessageBoxEx.Show("Yeah!");
        }
    }
}
