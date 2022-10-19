using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading.Tasks;

namespace HotPin.Commands
{
    public enum KillType
    {
        Contain,
        Regex
    }

    public class Kill : Command
    {
        static readonly Image KillImage = Resources.Kill;

        public override Image Image => KillImage;

        public KillType Type { get; set; } = KillType.Contain;
        public string Match { get; set; } = "SomeProcessName";

        protected override async Task OnExecute()
        {
            Process[] runningProcesses = Process.GetProcesses();
            foreach (Process process in runningProcesses)
            {
                switch (Type)
                {
                    case KillType.Contain:
                        if (process.ProcessName.ToLower().Contains(Match.ToLower()))
                        {
                            try
                            {
                                process.CloseMainWindow();
                                process.Kill();
                            }
                            catch { }
                        }
                        break;
                    case KillType.Regex:
                        throw new NotImplementedException();
                    default:
                        throw new NotImplementedException();
                }
            }
        }
    }
}
