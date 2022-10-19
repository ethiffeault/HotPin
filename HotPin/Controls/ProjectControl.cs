using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using JsonObject = System.Collections.Generic.Dictionary<string, object>;

namespace HotPin
{
    public partial class ProjectControl : UserControl
    {
        public Action OnProjectChanged;
        public bool Dirty { get; private set; } = false;

        private TreeNode selectedNode = null;
        private bool selectedNodeDirty = false;
        private Dictionary<Image, int> treeViewImagesIndices = new Dictionary<Image, int>();
        private bool setJsonControlTextInProgress = false;

        public ProjectControl()
        {
            InitializeComponent();

            SetSelectedNodeDirty(false);

            treeView.SetIsLeakCallback(TreeViewIsLeafCallback);
            treeView.SetCanInsertAtRoot(TreeViewCanInsertAtRoot);
            treeView.OnChanges += TreeViewOnChanges;
            treeView.MouseUp += TreeViewMouseUp;
            treeView.LostFocus += TreeViewLostFocus;
            treeView.GotFocus += TreeViewGotFocus;

            jsonControl.ContentChanged += JsonControlContentChanged;

            contextMenuPlaylistAddCommand.Image = Application.Resources.Plus;
            contextMenuPlaylistDelete.Image = Application.Resources.Minus;
            contextMenuFolderAddFolder.Image = Application.Resources.Plus;
            contextMenuFolderAddPlaylist.Image = Application.Resources.Playlist;
            contextMenuFolderDelete.Image = Application.Resources.Minus;
            contextMenuCommandDelete.Image = Application.Resources.Minus;

        }

        private void SetSelectedNodeDirty(bool value)
        {
            selectedNodeDirty = value;
            if (!setJsonControlTextInProgress && selectedNodeDirty)
                OnProjectChanged?.Invoke();
        }

        private void JsonControlContentChanged()
        {
            SetSelectedNodeDirty(true);
        }

        private void TreeViewGotFocus(object sender, EventArgs e)
        {
            UpdateSelectedNode();
        }

        private void TreeViewLostFocus(object sender, EventArgs e)
        {
            UpdateSelectedNode();
        }

        private void TreeViewMouseUp(object sender, MouseEventArgs e)
        {
            UpdateSelectedNode();
        }

        private void UpdateSelectedNode()
        {
            if (selectedNode != treeView.SelectedNode)
            {
                ApplyNodeChange();
                selectedNode = treeView.SelectedNode;
                OnSelectedNodeChange();
            }
        }

        private void SetSelectecedNode(TreeNode node)
        {
            treeView.SelectedNode = node;
            UpdateSelectedNode();
        }

        private void ApplyNodeChange()
        {
            if (selectedNode == null)
                return;

            if (!selectedNodeDirty)
                return;
            selectedNodeDirty = false;

            string jsonStr = jsonControl.GetText();

            JsonObject json = Json.ToJsonObject(jsonStr);
            if (json == null)
                return;

            json.Add("@type", selectedNode.Tag.GetType().FullName);

            Item item = Json.ToType<Item>(json);

            if (item == null || item.GetType() != selectedNode.Tag.GetType())
            {
                return;
            }

            selectedNode.Tag = item;
            selectedNode.Text = item.ToString();
            ProjectChanged();
        }

        private string GetNodeJson(TreeNode node)
        {
            JsonObject json = Json.ToJson(node.Tag, true) as JsonObject;

            if (json == null)
                return "";

            json.Remove("@type");
            json.Remove("Children");
            json.Remove("Commands");

            return Json.JsonToString(json, true);
        }

        private void OnSelectedNodeChange()
        {
            if (selectedNode == null)
            {
                SetJsonControlText("", true);
            }
            else
            {
                SetJsonControlText(GetNodeJson(selectedNode), false);
            }

            SetSelectedNodeDirty(false);
        }

        private void SetJsonControlText(string json, bool readOnly)
        {
            setJsonControlTextInProgress = true;
            jsonControl.SetText(json, readOnly);
            setJsonControlTextInProgress = false;
        }

        private List<int> GetNodeIndicesPath(TreeNode node)
        {
            List<int> indicesPath = new List<int>();
            TreeNode n = selectedNode;
            while (n != null)
            {
                indicesPath.Insert(0, n.Index);
                n = n.Parent;
            }
            return indicesPath;
        }

        private TreeNode GetNodeFromIndicesPath(List<int> indicesPath)
        {
            TreeNode n = null;
            if (indicesPath.Count > 0)
            {
                if (indicesPath[0] < treeView.Nodes.Count)
                {
                    n = treeView.Nodes[indicesPath[0]];
                }
            }

            if (n != null)
            {
                for (int i = 1; i < indicesPath.Count; i++)
                {
                    if (indicesPath[i] < n.Nodes.Count)
                    {
                        n = n.Nodes[indicesPath[i]];
                    }
                    else
                    {
                        break;
                    }
                }
            }
            return n;
        }

        public void ReadProject()
        {
            List<int> selectedIndicesPath = null;

            if (selectedNode != null)
                selectedIndicesPath = GetNodeIndicesPath(selectedNode);

            treeView.Nodes.Clear();

            foreach (Item item in Application.Instance.Project.Items)
            {
                TreeNode treeNode = LoadItem(item);
                if (treeNode != null)
                    treeView.Nodes.Add(treeNode);
            }

            treeView.ExpandAll();

            selectedNodeDirty = false;
            UpdateSelectedNode();

            if (selectedIndicesPath != null)
            {
                TreeNode n = GetNodeFromIndicesPath(selectedIndicesPath);
                if (n != null)
                {
                    treeView.SelectedNode = n;
                    UpdateSelectedNode();
                }
            }

            Dirty = false;
        }

        private TreeNode GetTreeNode(TreeNodeCollection nodes, string path)
        {
            foreach (TreeNode node in nodes)
            {
                if (node.FullPath == path)
                    return node;

                TreeNode child = GetTreeNode(node.Nodes, path);
                if (child != null)
                    return child;
            }
            return null;
        }

        public void WriteProject()
        {
            ApplyNodeChange();

            Project project = Application.Instance.Project;
            project.Items.Clear();
            foreach (TreeNode childNode in treeView.Nodes)
            {
                if (childNode.Tag is Item item)
                {
                    if (item is Playlist childPlaylist)
                    {
                        project.Items.Add(childPlaylist);
                        WritePlaylist(childNode, childPlaylist);
                    }
                    else if (item is Folder childFolder)
                    {
                        project.Items.Add(childFolder);
                        WriteFolder(childNode, childFolder);
                    }
                }
            }
            Dirty = false;
        }

        private void WritePlaylist(TreeNode node, Playlist playlist)
        {
            playlist.Commands.Clear();
            foreach (TreeNode childNode in node.Nodes)
            {
                // may only contain command
                if (childNode.Tag is Command command)
                {
                    playlist.Commands.Add(command);
                    WriteCommand(childNode, command);
                }
            }
        }

        private void WriteFolder(TreeNode node, Folder folder)
        {
            folder.Children.Clear();
            foreach (TreeNode childNode in node.Nodes)
            {
                // may only contain command
                if (childNode.Tag is Item item)
                {
                    if (item is Playlist childPlaylist)
                    {
                        folder.Children.Add(childPlaylist);
                        WritePlaylist(childNode, childPlaylist);
                    }
                    else if (item is Folder childFolder)
                    {
                        folder.Children.Add(childFolder);
                        WriteFolder(childNode, childFolder);
                    }
                }
            }
        }

        private void WriteCommand(TreeNode node, Command command)
        {
            // nothing to do for now
        }

        private void ProjectChanged()
        {
            if (Dirty == false)
            {
                Dirty = true;
            }
            OnProjectChanged?.Invoke();
        }

        private void TreeViewOnChanges()
        {
            ProjectChanged();
            UpdateSelectedNode();
        }

        private bool TreeViewIsLeafCallback(TreeNode parent, TreeNode child)
        {
            if (parent.Tag is Folder && !(child.Tag is Command))
            {
                return true;
            }

            if (parent.Tag is Playlist && child.Tag is Command)
            {
                return true;
            }

            return false;
        }

        private bool TreeViewCanInsertAtRoot(TreeNode node)
        {
            return node.Tag is Folder || node.Tag is Playlist;
        }

        private int GetImageIndex(Image image)
        {
            if (treeView.ImageList == null)
            {
                treeView.ImageList = new ImageList();
            }

            if (treeViewImagesIndices.TryGetValue(image, out int index))
            {
                return index;
            }

            index = treeView.ImageList.Images.Count;
            treeView.ImageList.Images.Add(image);
            treeViewImagesIndices.Add(image, index);
            return index;
        }

        private TreeNode CreateTreeNode(Folder folder)
        {
            TreeNode treeNode = new TreeNode();
            treeNode.Text = folder.ToString();
            treeNode.Tag = folder;
            treeNode.ImageIndex = GetImageIndex(folder.Image);
            treeNode.SelectedImageIndex = GetImageIndex(folder.Image);
            treeNode.ContextMenuStrip = contextMenuFolder;
            return treeNode;
        }

        private TreeNode CreateTreeNode(Playlist playlist)
        {
            TreeNode treeNode = new TreeNode();
            treeNode.Text = playlist.ToString();
            treeNode.Tag = playlist;
            treeNode.ImageIndex = GetImageIndex(playlist.Image);
            treeNode.SelectedImageIndex = GetImageIndex(playlist.Image);
            treeNode.ContextMenuStrip = contextMenuPlaylist;
            return treeNode;
        }

        private TreeNode CreateTreeNode(Command command)
        {
            TreeNode treeNode = new TreeNode();
            treeNode.Text = command.ToString();
            treeNode.Tag = command;
            treeNode.ImageIndex = GetImageIndex(command.Image);
            treeNode.SelectedImageIndex = GetImageIndex(command.Image);
            treeNode.ContextMenuStrip = contextMenuCommand;
            return treeNode;
        }

        private TreeNode LoadItem(Item item, bool clone = true)
        {
            TreeNode treeNode = null;
            if (item is Playlist itemPlaylist)
            {
                Playlist playlist = clone ? Item.Clone(itemPlaylist) : itemPlaylist;
                treeNode = CreateTreeNode(playlist);
                foreach (Command command in itemPlaylist.Commands)
                {
                    TreeNode childNode = LoadItem(command, false);
                    if (childNode != null)
                        treeNode.Nodes.Add(childNode);
                }
            }
            else if (item is Command itemCommand)
            {
                Command command = clone ? Item.Clone(itemCommand) : itemCommand;
                treeNode = CreateTreeNode(command);
            }
            else if (item is Folder itemFolder)
            {

                Folder folder = clone ? Item.Clone(itemFolder) : itemFolder;
                treeNode = CreateTreeNode(folder);
                foreach (Item child in folder.Children)
                {
                    TreeNode childNode = LoadItem(child, false);
                    if (childNode != null)
                        treeNode.Nodes.Add(childNode);
                }
            }
            else
            {
                throw new NotImplementedException();
            }
            return treeNode;
        }

        private void ButtonApplyClick(object sender, EventArgs e)
        {
            ApplyNodeChange();
        }

        private void ButtonUndoClick(object sender, EventArgs e)
        {
            if (selectedNode != null)
            {
                SetJsonControlText(GetNodeJson(selectedNode), false);
                SetSelectedNodeDirty(false);
            }
        }

        private void ContextMenuDeleteClick(object sender, EventArgs e)
        {
            if (e is MouseEventArgs mouseEventArgs)
            {
                if (mouseEventArgs.Button != MouseButtons.Left)
                    return;
            }
            if (selectedNode == null)
                return;

            selectedNode.Remove();
        }

        private void AddNewFolder()
        {
            TreeNodeCollection parent = null;

            if (selectedNode == null)
            {
                parent = treeView.Nodes;
            }
            else if (selectedNode.Tag is Folder)
            {
                parent = selectedNode.Nodes;
            }

            if (parent != null)
            {
                Folder newFolder = new Folder();
                newFolder.Name = "New Folder";
                TreeNode treeNode = CreateTreeNode(newFolder);
                parent.Add(treeNode);
                SetSelectecedNode(treeNode);
            }
        }

        private void AddNewPlaylist()
        {
            if (selectedNode == null)
                return;

            if (selectedNode.Tag is Folder)
            {
                Playlist newPlaylist = new Playlist();
                newPlaylist.Name = "New Playlist";
                TreeNode treeNode = CreateTreeNode(newPlaylist);
                selectedNode.Nodes.Add(treeNode);
                SetSelectecedNode(treeNode);
            }
        }

        private void AddNewCommand(Command command)
        {
            if (selectedNode == null)
                return;

            if (selectedNode.Tag is Playlist)
            {
                TreeNode treeNode = CreateTreeNode(command);
                selectedNode.Nodes.Add(treeNode);
                SetSelectecedNode(treeNode);
            }
        }

        private void ContextMenuFolderAddFolderClick(object sender, EventArgs e)
        {
            if (e is MouseEventArgs mouseEventArgs)
            {
                if (mouseEventArgs.Button != MouseButtons.Left)
                    return;
            }
            AddNewFolder();
        }

        private void ContextMenuFolderPlaylistClick(object sender, EventArgs e)
        {
            if (e is MouseEventArgs mouseEventArgs)
            {
                if (mouseEventArgs.Button != MouseButtons.Left)
                    return;
            }
            AddNewPlaylist();
        }

        private void ContextMenuPlaylistOpening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            List<Command> commands = Application.Instance.Project.GetItemsOfType<Command>();

            List<Type> commandTypes = Utils.GetTypeInAssemblies(typeof(Command), false, Application.Instance.CommandAssemblies.ToArray());

            contextMenuPlaylistAddCommand.DropDownItems.Clear();
            foreach (Type commandType in commandTypes)
            {
                Command command = Activator.CreateInstance(commandType) as Command;
                command.Name = $"New {commandType.Name}";
                ToolStripMenuItem menuItem = new ToolStripMenuItem(commandType.Name, command.Image);
                menuItem.Click += new EventHandler(
                delegate (object menuItemSender, EventArgs menuItemEvent)
                {
                    AddNewCommand(command);
                });
                contextMenuPlaylistAddCommand.DropDownItems.Add(menuItem);
            }
        }

    }
}
