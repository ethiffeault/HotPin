using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotPin
{
    public class Executor
    {
        private Dictionary<HotKey, Playlist> hotKeyPlaylist = new Dictionary<HotKey, Playlist>();
        private HotKeyForm hotKeyForm;
        private HashSet<Playlist> exclusiveRunningPlaylists = new HashSet<Playlist>();
        private int runningCount = 0;
        public bool IsRunnig { get => runningCount != 0; }

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
                    flags |= playlist.Modifiers[i];

                HotKey hotKey = new HotKey(playlist.Key, flags);
                if (hotKeyPlaylist.ContainsKey(hotKey))
                    continue;

                hotKeyForm.RegisterHotKey(hotKey);
                hotKeyPlaylist.Add(hotKey, playlist);
            }
        }

        public void RunPlaylist(Playlist playlist)
        {
            _ = RunPlaylistAsync(playlist);
        }

        public async Task RunPlaylistAsync(Playlist playlist)
        {
            if (playlist.Exclusive)
            {
                if (exclusiveRunningPlaylists.Contains(playlist))
                    return;
                exclusiveRunningPlaylists.Add(playlist);
                ++runningCount;
                await playlist.Execute();
                --runningCount;
                exclusiveRunningPlaylists.Remove(playlist);
            }
            else
            {
                ++runningCount;
                await playlist.Execute();
                --runningCount;
            }
        }

        public void RunCommand(Command command)
        {
            ++runningCount;
            _ = command.Execute();
            --runningCount;
        }

        public async Task RunCommandAsync(Command command)
        {
            ++runningCount;
            await command.Execute();
            --runningCount;
        }
    }
}
