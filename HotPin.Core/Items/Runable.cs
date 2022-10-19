using System.Threading.Tasks;

namespace HotPin
{
    public abstract class Runable : Item
    {
        protected abstract Task OnExecute();

        public async Task Execute()
        {
            Log.Info($"Start {GetType().Name}:{ToLog()}", "Command");
            await OnExecute();
            Log.Info($"Done {GetType().Name}:{ToLog()}", "Command");
        }

        public virtual string ToLog()
        {
            return ToString();
        }
    }
}
