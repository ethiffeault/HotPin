using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HotPin
{
    public class Playlist : Runable
    {
        static readonly Image PlaylistImage = Core.Resources.Playlist;

        public override Image Image { get => PlaylistImage; }

        public Keys Key { get; set; } = Keys.H;

        public List<HotKeyModifiers> Modifiers { get; set; } = new List<HotKeyModifiers>() { HotKeyModifiers.Control };

        public bool Exclusive { get; set; } = true;

        public List<Command> Commands { get; set; } = new List<Command>();

        public override List<Item> GetChildren()
        {
            return Commands.Cast<Item>().ToList();
        }

        protected override async Task OnExecute()
        {
            foreach (Command command in Commands)
            {
                await command.Execute();
            }
        }
    }
}
