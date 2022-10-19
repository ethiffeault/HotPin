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
            this.contextMenuPlaylistRun = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuCommand = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextMenuCommandDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuCommandRun = new System.Windows.Forms.ToolStripMenuItem();
            this.jsonControl = new HotPin.Controls.JsonControl();
            this.treeView = new HotPin.Controls.TreeView();
            this.textBoxDocumentation = new System.Windows.Forms.TextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
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
            this.contextMenuFolder.Opening += new System.ComponentModel.CancelEventHandler(this.ContextMenuFolderOpening);
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
            this.contextMenuPlaylistDelete,
            this.contextMenuPlaylistRun});
            this.contextMenuPlaylist.Name = "contextMenuPlaylist";
            this.contextMenuPlaylist.Size = new System.Drawing.Size(166, 70);
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
            // contextMenuPlaylistRun
            // 
            this.contextMenuPlaylistRun.Name = "contextMenuPlaylistRun";
            this.contextMenuPlaylistRun.Size = new System.Drawing.Size(165, 22);
            this.contextMenuPlaylistRun.Text = "Run";
            this.contextMenuPlaylistRun.Click += new System.EventHandler(this.contextMenuPlaylistRun_Click);
            // 
            // contextMenuCommand
            // 
            this.contextMenuCommand.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contextMenuCommandDelete,
            this.contextMenuCommandRun});
            this.contextMenuCommand.Name = "contextMenuCommand";
            this.contextMenuCommand.Size = new System.Drawing.Size(108, 48);
            // 
            // contextMenuCommandDelete
            // 
            this.contextMenuCommandDelete.Name = "contextMenuCommandDelete";
            this.contextMenuCommandDelete.Size = new System.Drawing.Size(107, 22);
            this.contextMenuCommandDelete.Text = "Delete";
            this.contextMenuCommandDelete.Click += new System.EventHandler(this.ContextMenuDeleteClick);
            // 
            // contextMenuCommandRun
            // 
            this.contextMenuCommandRun.Name = "contextMenuCommandRun";
            this.contextMenuCommandRun.Size = new System.Drawing.Size(107, 22);
            this.contextMenuCommandRun.Text = "Run";
            this.contextMenuCommandRun.Click += new System.EventHandler(this.ContextMenuCommandRunClick);
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
            this.jsonControl.Size = new System.Drawing.Size(325, 406);
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
            this.treeView.Size = new System.Drawing.Size(267, 406);
            this.treeView.TabIndex = 0;
            // 
            // textBoxDocumentation
            // 
            this.textBoxDocumentation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxDocumentation.Location = new System.Drawing.Point(607, 3);
            this.textBoxDocumentation.Multiline = true;
            this.textBoxDocumentation.Name = "textBoxDocumentation";
            this.textBoxDocumentation.ReadOnly = true;
            this.textBoxDocumentation.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.textBoxDocumentation.Size = new System.Drawing.Size(310, 406);
            this.textBoxDocumentation.TabIndex = 3;
            this.textBoxDocumentation.WordWrap = false;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // ProjectControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.textBoxDocumentation);
            this.Controls.Add(this.jsonControl);
            this.Controls.Add(this.treeView);
            this.DoubleBuffered = true;
            this.Name = "ProjectControl";
            this.Size = new System.Drawing.Size(920, 415);
            this.contextMenuFolder.ResumeLayout(false);
            this.contextMenuPlaylist.ResumeLayout(false);
            this.contextMenuCommand.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

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
        private System.Windows.Forms.ToolStripMenuItem contextMenuCommandRun;
        private System.Windows.Forms.ToolStripMenuItem contextMenuPlaylistRun;
        private System.Windows.Forms.TextBox textBoxDocumentation;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
    }
}
