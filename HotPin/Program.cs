using System;

namespace HotPin
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);

            Application.Instance.Init(new MainForm());
            System.Windows.Forms.Application.Run(Application.Instance);
        }
    }
}
