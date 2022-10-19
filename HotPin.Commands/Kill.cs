using System;
using System.Diagnostics;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HotPin.Commands
{
    public enum KillType
    {
        Contain,
        Equal,
        Regex
    }

    public class Kill : Command
    {
        static readonly Image KillImage = Resources.Kill;
        public override Image Image => KillImage;

        [Description(
@"How to match the process name
    Contain: check if Match is in the process name
    Equal: Match need to be exactly process name
    Regex: perform a regex comparison using Match")]
        public KillType Type { get; set; } = KillType.Contain;

        [Description("Compare string again process name")]
        public string Match { get; set; } = "SomeProcessName";

        protected override async Task OnExecute()
        {
            Process[] runningProcesses = Process.GetProcesses();
            foreach (Process process in runningProcesses)
            {
                bool kill = false;
                switch (Type)
                {
                    case KillType.Contain:
                        if (process.ProcessName.ToLower().Contains(Match.ToLower()))
                            kill = true;
                        break;
                    case KillType.Equal:
                        if (process.ProcessName == Match)
                            kill = true;
                        break;
                    case KillType.Regex:
                        try
                        {
                            if (Regex.Match(process.ProcessName, Match).Success)
                                kill = true;
                        }
                        catch { }
                        throw new NotImplementedException();
                    default:
                        throw new NotImplementedException();
                }

                if (kill)
                {
                    try
                    {
                        process.CloseMainWindow();
                        process.Kill();
                    }
                    catch { }
                }
            }
        }
    }
}
