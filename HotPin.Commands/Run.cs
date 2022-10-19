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

        [Parameter(Description = "exe full path")]
        public string Path { get; set; } = "c:\\some\\path\\app.exe";

        [Parameter(Description = "working directory of the exe, may be empty")]
        public string WorkingDir { get; set; } = "";

        [Parameter(Description = "arguments passed to the exe")]
        public string Arguments { get; set; } = "";

        [Parameter(Description = "wait for exe to exit to execute next command")]
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
            {
                await Task.Run(() => { process.WaitForExit(); });
            }
        }
    }
}
