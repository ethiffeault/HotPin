using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;

namespace HotPin
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            using (Mutex mutex = new Mutex(false, "Global\\99D17104-9ECD-4124-A5FF-1A7EA320467B"))
            {
                if (mutex.WaitOne(0, false))
                {
                    System.Windows.Forms.Application.EnableVisualStyles();
                    System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);

                    Application.Instance.Init(new MainForm());
                    System.Windows.Forms.Application.Run(Application.Instance);
                }
                else if (System.Diagnostics.Debugger.IsAttached)
                {
                    MessageBoxEx.Show("Already running... exiting...", Application.Name);
                }
            }
        }
    }
}
