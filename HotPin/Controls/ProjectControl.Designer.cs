namespace HotPin
{
    partial class ProjectControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.contextMenuFolder = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextMenuFolderAddFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuFolderAddPlaylist = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuFolderDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuPlaylist = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextMenuPlaylistAddCommand = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuPlaylistDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuCommand = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextMenuCommandDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.jsonControl = new HotPin.Controls.JsonControl();
            this.treeView = new HotPin.Controls.TreeView();
            this.contextMenuFolder.SuspendLayout();
            this.contextMenuPlaylist.SuspendLayout();
            this.contextMenuCommand.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuFolder
            // 
            this.contextMenuFolder.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contextMenuFolderAddFolder,
            this.contextMenuFolderAddPlaylist,
            this.contextMenuFolderDelete});
            this.contextMenuFolder.Name = "contextMenuStrip1";
            this.contextMenuFolder.Size = new System.Drawing.Size(137, 70);
            // 
            // contextMenuFolderAddFolder
            // 
            this.contextMenuFolderAddFolder.Name = "contextMenuFolderAddFolder";
            this.contextMenuFolderAddFolder.Size = new System.Drawing.Size(136, 22);
            this.contextMenuFolderAddFolder.Text = "Add Folder";
            this.contextMenuFolderAddFolder.Click += new System.EventHandler(this.ContextMenuFolderAddFolderClick);
            // 
            // contextMenuFolderAddPlaylist
            // 
            this.contextMenuFolderAddPlaylist.Name = "contextMenuFolderAddPlaylist";
            this.contextMenuFolderAddPlaylist.Size = new System.Drawing.Size(136, 22);
            this.contextMenuFolderAddPlaylist.Text = "Add Playlist";
            this.contextMenuFolderAddPlaylist.Click += new System.EventHandler(this.ContextMenuFolderPlaylistClick);
            // 
            // contextMenuFolderDelete
            // 
            this.contextMenuFolderDelete.Name = "contextMenuFolderDelete";
            this.contextMenuFolderDelete.Size = new System.Drawing.Size(136, 22);
            this.contextMenuFolderDelete.Text = "Delete";
            this.contextMenuFolderDelete.Click += new System.EventHandler(this.ContextMenuDeleteClick);
            // 
            // contextMenuPlaylist
            // 
            this.contextMenuPlaylist.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contextMenuPlaylistAddCommand,
            this.contextMenuPlaylistDelete});
            this.contextMenuPlaylist.Name = "contextMenuPlaylist";
            this.contextMenuPlaylist.Size = new System.Drawing.Size(166, 48);
            this.contextMenuPlaylist.Opening += new System.ComponentModel.CancelEventHandler(this.ContextMenuPlaylistOpening);
            // 
            // contextMenuPlaylistAddCommand
            // 
            this.contextMenuPlaylistAddCommand.Name = "contextMenuPlaylistAddCommand";
            this.contextMenuPlaylistAddCommand.Size = new System.Drawing.Size(165, 22);
            this.contextMenuPlaylistAddCommand.Text = "Add Command...";
            // 
            // contextMenuPlaylistDelete
            // 
            this.contextMenuPlaylistDelete.Name = "contextMenuPlaylistDelete";
            this.contextMenuPlaylistDelete.Size = new System.Drawing.Size(165, 22);
            this.contextMenuPlaylistDelete.Text = "Delete";
            this.contextMenuPlaylistDelete.Click += new System.EventHandler(this.ContextMenuDeleteClick);
            // 
            // contextMenuCommand
            // 
            this.contextMenuCommand.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contextMenuCommandDelete});
            this.contextMenuCommand.Name = "contextMenuCommand";
            this.contextMenuCommand.Size = new System.Drawing.Size(108, 26);
            this.contextMenuCommand.Click += new System.EventHandler(this.ContextMenuDeleteClick);
            // 
            // contextMenuCommandDelete
            // 
            this.contextMenuCommandDelete.Name = "contextMenuCommandDelete";
            this.contextMenuCommandDelete.Size = new System.Drawing.Size(107, 22);
            this.contextMenuCommandDelete.Text = "Delete";
            this.contextMenuCommandDelete.Click += new System.EventHandler(this.ContextMenuDeleteClick);
            // 
            // jsonControl
            // 
            this.jsonControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.jsonControl.Bookmark = true;
            this.jsonControl.BottomContextText = true;
            this.jsonControl.HScrollBar = true;
            this.jsonControl.Location = new System.Drawing.Point(276, 3);
            this.jsonControl.Name = "jsonControl";
            this.jsonControl.NumberMarge = false;
            this.jsonControl.Size = new System.Drawing.Size(357, 351);
            this.jsonControl.TabIndex = 2;
            // 
            // treeView
            // 
            this.treeView.AllowDrop = true;
            this.treeView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.treeView.ContextMenuStrip = this.contextMenuFolder;
            this.treeView.Location = new System.Drawing.Point(3, 3);
            this.treeView.Name = "treeView";
            this.treeView.Size = new System.Drawing.Size(267, 334);
            this.treeView.TabIndex = 0;
            // 
            // ProjectControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.jsonControl);
            this.Controls.Add(this.treeView);
            this.DoubleBuffered = true;
            this.Name = "ProjectControl";
            this.Size = new System.Drawing.Size(641, 343);
            this.contextMenuFolder.ResumeLayout(false);
            this.contextMenuPlaylist.ResumeLayout(false);
            this.contextMenuCommand.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private HotPin.Controls.TreeView treeView;
        private Controls.JsonControl jsonControl;
        private System.Windows.Forms.ContextMenuStrip contextMenuFolder;
        private System.Windows.Forms.ToolStripMenuItem contextMenuFolderDelete;
        private System.Windows.Forms.ToolStripMenuItem contextMenuFolderAddFolder;
        private System.Windows.Forms.ToolStripMenuItem contextMenuFolderAddPlaylist;
        private System.Windows.Forms.ContextMenuStrip contextMenuPlaylist;
        private System.Windows.Forms.ContextMenuStrip contextMenuCommand;
        private System.Windows.Forms.ToolStripMenuItem contextMenuPlaylistDelete;
        private System.Windows.Forms.ToolStripMenuItem contextMenuCommandDelete;
        private System.Windows.Forms.ToolStripMenuItem contextMenuPlaylistAddCommand;
    }
}
