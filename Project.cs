using System.Collections.Generic;
using System.IO;

namespace HotPin
{
    public class Project
    {
        public static string ProjectFile = Path.Combine(new FileInfo(System.Reflection.Assembly.GetEntryAssembly().Location).DirectoryName, "HotPin.json");

        public List<Item> Items { get; set; } = new List<Item>();

        public static Project Load()
        {
            Project project = null;

            if (File.Exists(ProjectFile))
            {
                project = Json.ToType<Project>(File.ReadAllText(ProjectFile));
            }

            if (project == null)
            {
                project = new Project();
                project.Items.Add(new Commands.DialogBox());
                Folder fx3 = new Folder() { Name = "FX3" };

                fx3.Children.Add(new Commands.DialogBox());
                fx3.Children.Add(new Commands.DialogBox());
                fx3.Children.Add(new Folder() { Name = "Dmd" });

                Playlist playlist = new Playlist() { Name = "PlayList" };
                fx3.Children.Add(playlist);
                playlist.Commands.Add(new Commands.DialogBox());

                project.Items.Add(fx3);
            }

            return project;
        }

        public static void Save(Project project)
        {
            string json = Json.ToString(project, true, true);
            if (json != null)
                File.WriteAllText(ProjectFile, json);
        }

        public List<T> GetItemsOfType<T>()
        {
            List<T> playlists = new List<T>();

            foreach (Item item in Items)
            {
                GetItemsOfType(item, playlists);
            }
            return playlists;
        }

        private void GetItemsOfType<T>(Item item, List<T> playlists)
        {
            if (item is T t)
            {
                playlists.Add(t);
            }

            foreach (Item child in item.GetChildren())
            {
                GetItemsOfType(child, playlists);
            }
        }
    }


}
