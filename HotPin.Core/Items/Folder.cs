using System.Collections.Generic;
using System.Drawing;

namespace HotPin
{
    public class Folder : Item
    {
        static readonly Image FolderImage = Core.Resources.Folder;

        public override Image Image { get => FolderImage; }

        public List<Item> Children { get; set; } = new List<Item>();

        public override List<Item> GetChildren()
        {
            return Children;
        }
    }
}
