using System;
using System.IO;

namespace HotPin
{
    public enum LogLevel
    {
        Error,
        Warning,
        Info,
    }

    public class LogSetting : Settings.Entry
    {
        public LogLevel LogLevel { get; set; }
    }

    public static class Log
    {
        public static LogSetting Setting { get => Settings.Get<LogSetting>(); }

        public static readonly string LogFile = Path.Combine(new FileInfo(System.Reflection.Assembly.GetEntryAssembly().Location).DirectoryName, "HotPin.log");

        public static string DefaultContext { get; set; } = "Log";

        static Log()
        {
            if (File.Exists(LogFile))
            {
                try
                {
                    File.Delete(LogFile);
                }
                catch { }
            }
        }

        public static void Info(string msg, string context = null)
        {
            Write(LogLevel.Info, msg, context);
        }

        public static void Warning(string msg, string context = null)
        {
            Write(LogLevel.Warning, msg, context);
        }

        public static void Error(string msg, string context = null)
        {
            Write(LogLevel.Error, msg, context);
        }

        public static void Write(LogLevel level, string msg, string context)
        {
            context = context ?? DefaultContext;

            if (level <= Setting.LogLevel)
            {
                DateTime time = DateTime.Now;
                string output = null;
                if (context == null)
                    output = string.Format("{0:00}:{1:00}:{2:00} {3,-7} {4}{5}", time.Hour, time.Minute, time.Second, level, msg, System.Environment.NewLine);
                else
                    output = string.Format("{0:00}:{1:00}:{2:00} {3,-7} [{4, -12}] {5}{6}", time.Hour, time.Minute, time.Second, level, context, msg, System.Environment.NewLine);

                if (System.Diagnostics.Debugger.IsAttached)
                    System.Diagnostics.Debug.Write(output);

                try
                {
                    File.AppendAllText(LogFile, output);
                }
                catch { }
            }
        }

        public static void View()
        {
            if (File.Exists(Log.LogFile))
                System.Diagnostics.Process.Start(Log.LogFile);
        }
    }
}
