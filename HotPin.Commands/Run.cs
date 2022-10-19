using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;

namespace HotPin.Commands
{
    public class Run : Command
    {
        static readonly Image RunImage = Resources.Run;
        public override Image Image => RunImage;

        public string Path { get; set; } = "c:\\some\\path\\app.exe";
        public string WorkingDir { get; set; } = "";
        public string Arguments { get; set; } = "";
        public bool WaitForExit { get; set; } = false;

        protected override async Task OnExecute()
        {
            string cleanPath = Path.Replace(@"\\", @"\");
            if (!File.Exists(cleanPath))
            {
                Log.Error($"Invalid Path: {cleanPath}", nameof(Run));
                return;
            }

            if (!String.IsNullOrEmpty(WorkingDir) && !Directory.Exists(WorkingDir))
            {
                Log.Error($"Invalid WorkingDir: {WorkingDir}", nameof(Run));
                return;
            }

            Process process = new Process();
            process.StartInfo.FileName = cleanPath;
            if (!string.IsNullOrEmpty(WorkingDir))
                process.StartInfo.WorkingDirectory = WorkingDir;
            process.StartInfo.Arguments = Arguments;
            process.StartInfo.CreateNoWindow = true;
            process.Start();
            if (WaitForExit)
                process.WaitForExit();
        }
    }
}
