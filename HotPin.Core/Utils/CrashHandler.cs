using System;
using System.Threading;

namespace HotPin
{
    public static class CrashHandler
    {
        public static Action<Exception> Crashed;

        public static void Init(bool debugMode)
        {
            if (!debugMode)
            {
                System.Windows.Forms.Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);
                AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
            }
        }

        static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            // Log the exception, display it, etc
            HandleException(e.Exception);
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            // Log the exception, display it, etc
            HandleException(e.ExceptionObject as Exception);
        }

        static void HandleException(Exception e)
        {
            Log.Error($"{e.Message}{Environment.NewLine}{e.StackTrace}", "Crash");
            Crashed?.Invoke(e);
            Thread.Sleep(1000);
            Environment.Exit(1);
        }
    }
}
