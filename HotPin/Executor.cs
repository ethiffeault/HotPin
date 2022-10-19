using System.Collections.Generic;

namespace HotPin
{
    public class Executor
    {
        private Dictionary<HotKey, Playlist> hotKeyPlaylist = new Dictionary<HotKey, Playlist>();
        private HotKeyForm hotKeyForm;

        public Executor(HotKeyForm hotKeyForm)
        {
            this.hotKeyForm = hotKeyForm;
            Application.Instance.ProjectLoaded += ProjectLoaded;
            Application.Instance.ProjectSaved += ProjectSaved;
            Application.Instance.ProjectClosed += ProjectClosed;
            hotKeyForm.HotKeyPressed += HotKeyFormHotKeyPressed;
        }

        private void HotKeyFormHotKeyPressed(object sender, HotKeyEventArgs e)
        {
            if (hotKeyPlaylist.TryGetValue(e.HotKey, out Playlist playlist))
            {
                RunPlaylist(playlist);
            }
        }

        private void ProjectLoaded()
        {
            Refresh();
        }

        private void ProjectSaved()
        {
            Refresh();
        }

        private void ProjectClosed()
        {
            hotKeyPlaylist.Clear();
            hotKeyForm.UnregisterAll();
        }

        private void Refresh()
        {
            hotKeyPlaylist.Clear();
            hotKeyForm.UnregisterAll();

            foreach (Playlist playlist in Application.Instance.Project.GetItemsOfType<Playlist>())
            {
                if (playlist.Key == System.Windows.Forms.Keys.None)
                    continue;

                if (playlist.Modifiers.Count == 0)
                    continue;

                HotKeyModifiers flags = playlist.Modifiers[0];
                for (int i = 1; i < playlist.Modifiers.Count; ++i)
                    flags &= playlist.Modifiers[i];
                //flags &= HotKeyModifiers.NoRepeat;

                HotKey hotKey = new HotKey(playlist.Key, flags);

                if (hotKeyPlaylist.ContainsKey(hotKey))
                    continue;

                hotKeyForm.RegisterHotKey(hotKey);
                hotKeyPlaylist.Add(hotKey, playlist);
            }
        }

        public void RunPlaylist(Playlist playlist)
        {
            _ = playlist.Run();
        }

        public void RunCommand(Command command)
        {
            _ = command.Run();
        }
    }
}
