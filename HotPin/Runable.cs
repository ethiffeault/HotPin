using System.Threading.Tasks;

namespace HotPin
{
    public abstract class Runable : Item
    {
        public abstract Task Run();
    }
}
