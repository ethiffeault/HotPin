namespace HotPin
{
    partial class MainForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.projectControl = new HotPin.ProjectControl();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.FileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FileSaveMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FileLoadMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FileExitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DebugMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DebugLogLevelMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openLogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.separatorToolStripMenuItem = new System.Windows.Forms.ToolStripSeparator();
            this.levelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DebugLogLevelInfoMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DebugLogLevelWarningMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DebugLogLevelErrorMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // projectControl
            // 
            this.projectControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.projectControl.Location = new System.Drawing.Point(0, 24);
            this.projectControl.Name = "projectControl";
            this.projectControl.Size = new System.Drawing.Size(800, 426);
            this.projectControl.TabIndex = 0;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileMenuItem,
            this.DebugMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // FileMenuItem
            // 
            this.FileMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileSaveMenuItem,
            this.FileLoadMenuItem,
            this.toolStripMenuItem1,
            this.FileExitMenuItem});
            this.FileMenuItem.Name = "FileMenuItem";
            this.FileMenuItem.Size = new System.Drawing.Size(37, 20);
            this.FileMenuItem.Text = "&File";
            // 
            // FileSaveMenuItem
            // 
            this.FileSaveMenuItem.Name = "FileSaveMenuItem";
            this.FileSaveMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.FileSaveMenuItem.Size = new System.Drawing.Size(138, 22);
            this.FileSaveMenuItem.Text = "&Save";
            this.FileSaveMenuItem.Click += new System.EventHandler(this.MainMenuFileSaveClick);
            // 
            // FileLoadMenuItem
            // 
            this.FileLoadMenuItem.Name = "FileLoadMenuItem";
            this.FileLoadMenuItem.Size = new System.Drawing.Size(138, 22);
            this.FileLoadMenuItem.Text = "Load";
            this.FileLoadMenuItem.Click += new System.EventHandler(this.MainMenuFileLoad);
            // 
            // FileExitMenuItem
            // 
            this.FileExitMenuItem.Name = "FileExitMenuItem";
            this.FileExitMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.FileExitMenuItem.Size = new System.Drawing.Size(138, 22);
            this.FileExitMenuItem.Text = "E&xit";
            this.FileExitMenuItem.Click += new System.EventHandler(this.MainMenuFileExit);
            // 
            // DebugMenuItem
            // 
            this.DebugMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DebugLogLevelMenuItem});
            this.DebugMenuItem.Name = "DebugMenuItem";
            this.DebugMenuItem.Size = new System.Drawing.Size(54, 20);
            this.DebugMenuItem.Text = "Debug";
            // 
            // DebugLogLevelMenuItem
            // 
            this.DebugLogLevelMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openLogToolStripMenuItem,
            this.separatorToolStripMenuItem,
            this.levelToolStripMenuItem});
            this.DebugLogLevelMenuItem.Name = "DebugLogLevelMenuItem";
            this.DebugLogLevelMenuItem.Size = new System.Drawing.Size(180, 22);
            this.DebugLogLevelMenuItem.Text = "Log";
            this.DebugLogLevelMenuItem.DropDownOpened += new System.EventHandler(this.DebugLogLevelMenuItem_DropDownOpened);
            // 
            // openLogToolStripMenuItem
            // 
            this.openLogToolStripMenuItem.Name = "openLogToolStripMenuItem";
            this.openLogToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.openLogToolStripMenuItem.Text = "Open";
            this.openLogToolStripMenuItem.Click += new System.EventHandler(this.openLogToolStripMenuItem_Click);
            // 
            // separatorToolStripMenuItem
            // 
            this.separatorToolStripMenuItem.Name = "separatorToolStripMenuItem";
            this.separatorToolStripMenuItem.Size = new System.Drawing.Size(177, 6);
            // 
            // levelToolStripMenuItem
            // 
            this.levelToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DebugLogLevelInfoMenuItem,
            this.DebugLogLevelWarningMenuItem,
            this.DebugLogLevelErrorMenuItem});
            this.levelToolStripMenuItem.Name = "levelToolStripMenuItem";
            this.levelToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.levelToolStripMenuItem.Text = "Level";
            this.levelToolStripMenuItem.DropDownOpened += new System.EventHandler(this.DebugLogLevelMenuItem_DropDownOpened);
            // 
            // DebugLogLevelInfoMenuItem
            // 
            this.DebugLogLevelInfoMenuItem.Name = "DebugLogLevelInfoMenuItem";
            this.DebugLogLevelInfoMenuItem.Size = new System.Drawing.Size(180, 22);
            this.DebugLogLevelInfoMenuItem.Text = "Info";
            this.DebugLogLevelInfoMenuItem.Click += new System.EventHandler(this.DebugLogLevelInfoMenuItem_Click);
            // 
            // DebugLogLevelWarningMenuItem
            // 
            this.DebugLogLevelWarningMenuItem.Name = "DebugLogLevelWarningMenuItem";
            this.DebugLogLevelWarningMenuItem.Size = new System.Drawing.Size(180, 22);
            this.DebugLogLevelWarningMenuItem.Text = "Warning";
            this.DebugLogLevelWarningMenuItem.Click += new System.EventHandler(this.DebugLogLevelWarningMenuItem_Click);
            // 
            // DebugLogLevelErrorMenuItem
            // 
            this.DebugLogLevelErrorMenuItem.Name = "DebugLogLevelErrorMenuItem";
            this.DebugLogLevelErrorMenuItem.Size = new System.Drawing.Size(180, 22);
            this.DebugLogLevelErrorMenuItem.Text = "Error";
            this.DebugLogLevelErrorMenuItem.Click += new System.EventHandler(this.DebugLogLevelErrorMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(177, 6);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.projectControl);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "HotPin";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ProjectControl projectControl;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem FileMenuItem;
        private System.Windows.Forms.ToolStripMenuItem FileSaveMenuItem;
        private System.Windows.Forms.ToolStripMenuItem FileExitMenuItem;
        private System.Windows.Forms.ToolStripMenuItem FileLoadMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DebugMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DebugLogLevelMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openLogToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator separatorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem levelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DebugLogLevelInfoMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DebugLogLevelWarningMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DebugLogLevelErrorMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
    }
}

