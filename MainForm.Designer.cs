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
            this.separator1 = new System.Windows.Forms.ToolStripSeparator();
            this.separator2 = new System.Windows.Forms.ToolStripSeparator();
            this.projectControl = new HotPin.ProjectControl();
            this.menuStripMainMenu = new System.Windows.Forms.MenuStrip();
            this.menuItemFile = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemFileSave = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemFileLoad = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemFileExit = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemToolsConfiguration = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemToolsConfigurationOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemToolsConfigurationReload = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemDebug = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemDebugLog = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemDebugOpenLog = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemDebugLogLevel = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemDebugLogLevelInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemDebugLogLevelInfoWarning = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemDebugLogLevelInfoError = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStripMainMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // separator1
            // 
            this.separator1.Name = "separator1";
            this.separator1.Size = new System.Drawing.Size(135, 6);
            // 
            // separator2
            // 
            this.separator2.Name = "separator2";
            this.separator2.Size = new System.Drawing.Size(100, 6);
            // 
            // projectControl
            // 
            this.projectControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.projectControl.Location = new System.Drawing.Point(0, 24);
            this.projectControl.Name = "projectControl";
            this.projectControl.Size = new System.Drawing.Size(800, 426);
            this.projectControl.TabIndex = 0;
            // 
            // menuStripMainMenu
            // 
            this.menuStripMainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemFile,
            this.toolsToolStripMenuItem,
            this.menuItemDebug,
            this.helpToolStripMenuItem});
            this.menuStripMainMenu.Location = new System.Drawing.Point(0, 0);
            this.menuStripMainMenu.Name = "menuStripMainMenu";
            this.menuStripMainMenu.Size = new System.Drawing.Size(800, 24);
            this.menuStripMainMenu.TabIndex = 1;
            this.menuStripMainMenu.Text = "menuStrip1";
            // 
            // menuItemFile
            // 
            this.menuItemFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemFileSave,
            this.menuItemFileLoad,
            this.separator1,
            this.menuItemFileExit});
            this.menuItemFile.Name = "menuItemFile";
            this.menuItemFile.Size = new System.Drawing.Size(37, 20);
            this.menuItemFile.Text = "&File";
            // 
            // menuItemFileSave
            // 
            this.menuItemFileSave.Name = "menuItemFileSave";
            this.menuItemFileSave.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.menuItemFileSave.Size = new System.Drawing.Size(138, 22);
            this.menuItemFileSave.Text = "&Save";
            this.menuItemFileSave.Click += new System.EventHandler(this.MenuItemFileSaveClick);
            // 
            // menuItemFileLoad
            // 
            this.menuItemFileLoad.Name = "menuItemFileLoad";
            this.menuItemFileLoad.Size = new System.Drawing.Size(138, 22);
            this.menuItemFileLoad.Text = "Load";
            this.menuItemFileLoad.Click += new System.EventHandler(this.MenuItemFileLoad);
            // 
            // menuItemFileExit
            // 
            this.menuItemFileExit.Name = "menuItemFileExit";
            this.menuItemFileExit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.menuItemFileExit.Size = new System.Drawing.Size(138, 22);
            this.menuItemFileExit.Text = "E&xit";
            this.menuItemFileExit.Click += new System.EventHandler(this.MenuItemFileExit);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemToolsConfiguration});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // menuItemToolsConfiguration
            // 
            this.menuItemToolsConfiguration.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemToolsConfigurationOpen,
            this.menuItemToolsConfigurationReload});
            this.menuItemToolsConfiguration.Name = "menuItemToolsConfiguration";
            this.menuItemToolsConfiguration.Size = new System.Drawing.Size(148, 22);
            this.menuItemToolsConfiguration.Text = "Configuration";
            // 
            // menuItemToolsConfigurationOpen
            // 
            this.menuItemToolsConfigurationOpen.Name = "menuItemToolsConfigurationOpen";
            this.menuItemToolsConfigurationOpen.Size = new System.Drawing.Size(110, 22);
            this.menuItemToolsConfigurationOpen.Text = "Open";
            this.menuItemToolsConfigurationOpen.Click += new System.EventHandler(this.MenuItemToolsConfigurationOpenClick);
            // 
            // menuItemToolsConfigurationReload
            // 
            this.menuItemToolsConfigurationReload.Name = "menuItemToolsConfigurationReload";
            this.menuItemToolsConfigurationReload.Size = new System.Drawing.Size(110, 22);
            this.menuItemToolsConfigurationReload.Text = "Reload";
            this.menuItemToolsConfigurationReload.Click += new System.EventHandler(this.MenuItemToolsConfigurationReloadClick);
            // 
            // menuItemDebug
            // 
            this.menuItemDebug.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemDebugLog});
            this.menuItemDebug.Name = "menuItemDebug";
            this.menuItemDebug.Size = new System.Drawing.Size(54, 20);
            this.menuItemDebug.Text = "Debug";
            // 
            // menuItemDebugLog
            // 
            this.menuItemDebugLog.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemDebugOpenLog,
            this.separator2,
            this.menuItemDebugLogLevel});
            this.menuItemDebugLog.Name = "menuItemDebugLog";
            this.menuItemDebugLog.Size = new System.Drawing.Size(94, 22);
            this.menuItemDebugLog.Text = "Log";
            this.menuItemDebugLog.DropDownOpened += new System.EventHandler(this.MenuItemDebugLogLevelDropDownOpened);
            // 
            // menuItemDebugOpenLog
            // 
            this.menuItemDebugOpenLog.Name = "menuItemDebugOpenLog";
            this.menuItemDebugOpenLog.Size = new System.Drawing.Size(103, 22);
            this.menuItemDebugOpenLog.Text = "Open";
            this.menuItemDebugOpenLog.Click += new System.EventHandler(this.MenuItemDebugLogOpenClick);
            // 
            // menuItemDebugLogLevel
            // 
            this.menuItemDebugLogLevel.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemDebugLogLevelInfo,
            this.menuItemDebugLogLevelInfoWarning,
            this.menuItemDebugLogLevelInfoError});
            this.menuItemDebugLogLevel.Name = "menuItemDebugLogLevel";
            this.menuItemDebugLogLevel.Size = new System.Drawing.Size(103, 22);
            this.menuItemDebugLogLevel.Text = "Level";
            this.menuItemDebugLogLevel.DropDownOpened += new System.EventHandler(this.MenuItemDebugLogLevelDropDownOpened);
            // 
            // menuItemDebugLogLevelInfo
            // 
            this.menuItemDebugLogLevelInfo.Name = "menuItemDebugLogLevelInfo";
            this.menuItemDebugLogLevelInfo.Size = new System.Drawing.Size(119, 22);
            this.menuItemDebugLogLevelInfo.Text = "Info";
            this.menuItemDebugLogLevelInfo.Click += new System.EventHandler(this.MenuItemDebugLogLevelInfoClick);
            // 
            // menuItemDebugLogLevelInfoWarning
            // 
            this.menuItemDebugLogLevelInfoWarning.Name = "menuItemDebugLogLevelInfoWarning";
            this.menuItemDebugLogLevelInfoWarning.Size = new System.Drawing.Size(119, 22);
            this.menuItemDebugLogLevelInfoWarning.Text = "Warning";
            this.menuItemDebugLogLevelInfoWarning.Click += new System.EventHandler(this.MenuItemDebugLogLevelWarningClick);
            // 
            // menuItemDebugLogLevelInfoError
            // 
            this.menuItemDebugLogLevelInfoError.Name = "menuItemDebugLogLevelInfoError";
            this.menuItemDebugLogLevelInfoError.Size = new System.Drawing.Size(119, 22);
            this.menuItemDebugLogLevelInfoError.Text = "Error";
            this.menuItemDebugLogLevelInfoError.Click += new System.EventHandler(this.MenuItemDebugLogLevelErrorClick);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.aboutToolStripMenuItem.Text = "About...";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.MenuItemHelpAboutClick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.projectControl);
            this.Controls.Add(this.menuStripMainMenu);
            this.MainMenuStrip = this.menuStripMainMenu;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.Text = "HotPin";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.menuStripMainMenu.ResumeLayout(false);
            this.menuStripMainMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ProjectControl projectControl;
        private System.Windows.Forms.MenuStrip menuStripMainMenu;
        private System.Windows.Forms.ToolStripMenuItem menuItemFile;
        private System.Windows.Forms.ToolStripMenuItem menuItemFileSave;
        private System.Windows.Forms.ToolStripMenuItem menuItemFileExit;
        private System.Windows.Forms.ToolStripMenuItem menuItemFileLoad;
        private System.Windows.Forms.ToolStripMenuItem menuItemDebug;
        private System.Windows.Forms.ToolStripMenuItem menuItemDebugLog;
        private System.Windows.Forms.ToolStripMenuItem menuItemDebugOpenLog;
        private System.Windows.Forms.ToolStripMenuItem menuItemDebugLogLevel;
        private System.Windows.Forms.ToolStripMenuItem menuItemDebugLogLevelInfo;
        private System.Windows.Forms.ToolStripMenuItem menuItemDebugLogLevelInfoWarning;
        private System.Windows.Forms.ToolStripMenuItem menuItemDebugLogLevelInfoError;
        private System.Windows.Forms.ToolStripSeparator separator1;
        private System.Windows.Forms.ToolStripSeparator separator2;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuItemToolsConfiguration;
        private System.Windows.Forms.ToolStripMenuItem menuItemToolsConfigurationOpen;
        private System.Windows.Forms.ToolStripMenuItem menuItemToolsConfigurationReload;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
    }
}

