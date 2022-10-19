using System.Drawing;

namespace HotPin
{
    public abstract class Command : Runable
    {
        static readonly Image CommandImage = Core.Resources.Command;

        public override Image Image { get => CommandImage; }
    }
}
