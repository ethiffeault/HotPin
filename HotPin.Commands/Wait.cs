using System;
using System.Drawing;
using System.Threading.Tasks;

namespace HotPin.Commands
{
    public class Wait : Command
    {
        static readonly Image WaitImage = Resources.Wait;
        public override Image Image => WaitImage;

        public float TimeInSecond { get; set; } = 1.0f;

        protected override async Task OnExecute()
        {
            await Task.Delay(TimeSpan.FromSeconds(TimeInSecond));
        }

        public override string ToLog()
        {
            return $"{base.ToString()} {TimeInSecond} sec";
        }
    }
}
