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
            Assembly assembly = typeof(Program).Assembly;
            GuidAttribute guidAttribute = assembly.GetCustomAttributes(typeof(GuidAttribute), true)[0] as GuidAttribute;
            using (Mutex mutex = new Mutex(false, "Global\\" + guidAttribute.Value))
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
