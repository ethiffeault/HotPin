using System.Collections.Generic;
using System.Drawing;

namespace HotPin
{
    public abstract class Item
    {
        public string Name { get; set; }

        static readonly Image ItemImage = Properties.Resources.Default;

        [Json.NonSerialize]
        public virtual Image Image { get => ItemImage; }

        public Item()
        {
            Name = GetType().Name;
        }

        public static T Clone<T>(T item) where T : Item
        {
            string json = Json.ToString<T>(item, false, true);
            return Json.ToType<T>(json);
        }

        public override string ToString()
        {
            return Name;
        }

        public virtual List<Item> GetChildren()
        {
            return new List<Item>();
        }
    }
}
