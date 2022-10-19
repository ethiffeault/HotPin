using System;
using System.Collections.Generic;
using System.IO;

namespace HotPin
{
    public class Settings
    {
        private static Settings Instance { get; set; } = null;
        public static string SettingsFile { get => Path.Combine(new FileInfo(System.Reflection.Assembly.GetEntryAssembly().Location).DirectoryName, "HotPin.settings.json"); }

        private Dictionary<string, Entry> Entries = new Dictionary<string, Entry>();

        public class Entry
        {
            public int Version = 0;
        }

        static Settings()
        {
            Load();
        }

        public static T Get<T>(int version = 0) where T : Entry, new()
        {
            lock (Instance.Entries)
            {
                Entry entry = null;
                T setting = null;
                string key = typeof(T).FullName;

                if (Instance.Entries.TryGetValue(key, out entry))
                {
                    if (version == entry.Version)
                    {
                        setting = entry as T;
                    }
                }

                if (setting == null)
                {
                    setting = new T();
                    setting.Version = version;
                }

                Instance.Entries[key] = setting;

                return setting;
            }
        }

        public static void Load()
        {
            Settings settings = null;
            if (File.Exists(SettingsFile))
            {
                try
                {
                    settings = Json.ToType<Settings>(File.ReadAllText(SettingsFile));
                }
                catch (Exception)
                {
                }
            }

            if (settings == null)
            {
                settings = new Settings();
            }

            Instance = settings;
        }

        public static void Save()
        {
            try
            {
                File.WriteAllText(SettingsFile, Json.ToString(Instance, true, true));
            }
            catch (Exception ex)
            {
                Log.Error($"Error saving settings: {SettingsFile}, {ex.Message}");
            }
        }

        public static void Edit()
        {
            if (File.Exists(SettingsFile))
                System.Diagnostics.Process.Start(SettingsFile);
        }
    }
}
