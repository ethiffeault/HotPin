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
            this.components = new System.ComponentModel.Container();
            this.separator1 = new System.Windows.Forms.ToolStripSeparator();
            this.separator2 = new System.Windows.Forms.ToolStripSeparator();
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
            this.menuItemDebugLogOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemDebugLogLevel = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemDebugLogLevelInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemDebugLogLevelWarning = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemDebugLogLevelError = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemDebugCrash = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemHelpSendFeedback = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemHelpFeedbackProblem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemHelpFeedbackFeature = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemHelpAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemRunning = new System.Windows.Forms.ToolStripMenuItem();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.projectControl = new HotPin.ProjectControl();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.menuItemHelpViewHelp = new System.Windows.Forms.ToolStripMenuItem();
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
            // menuStripMainMenu
            // 
            this.menuStripMainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemFile,
            this.toolsToolStripMenuItem,
            this.menuItemDebug,
            this.helpToolStripMenuItem,
            this.menuItemRunning});
            this.menuStripMainMenu.Location = new System.Drawing.Point(0, 0);
            this.menuStripMainMenu.Name = "menuStripMainMenu";
            this.menuStripMainMenu.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            this.menuStripMainMenu.Size = new System.Drawing.Size(932, 24);
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
            this.menuItemDebugLog,
            this.menuItemDebugCrash});
            this.menuItemDebug.Name = "menuItemDebug";
            this.menuItemDebug.Size = new System.Drawing.Size(54, 20);
            this.menuItemDebug.Text = "Debug";
            // 
            // menuItemDebugLog
            // 
            this.menuItemDebugLog.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemDebugLogOpen,
            this.separator2,
            this.menuItemDebugLogLevel});
            this.menuItemDebugLog.Name = "menuItemDebugLog";
            this.menuItemDebugLog.Size = new System.Drawing.Size(104, 22);
            this.menuItemDebugLog.Text = "Log";
            this.menuItemDebugLog.DropDownOpened += new System.EventHandler(this.MenuItemDebugLogLevelDropDownOpened);
            // 
            // menuItemDebugLogOpen
            // 
            this.menuItemDebugLogOpen.Name = "menuItemDebugLogOpen";
            this.menuItemDebugLogOpen.Size = new System.Drawing.Size(103, 22);
            this.menuItemDebugLogOpen.Text = "Open";
            this.menuItemDebugLogOpen.Click += new System.EventHandler(this.MenuItemDebugLogOpenClick);
            // 
            // menuItemDebugLogLevel
            // 
            this.menuItemDebugLogLevel.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemDebugLogLevelInfo,
            this.menuItemDebugLogLevelWarning,
            this.menuItemDebugLogLevelError});
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
            // menuItemDebugLogLevelWarning
            // 
            this.menuItemDebugLogLevelWarning.Name = "menuItemDebugLogLevelWarning";
            this.menuItemDebugLogLevelWarning.Size = new System.Drawing.Size(119, 22);
            this.menuItemDebugLogLevelWarning.Text = "Warning";
            this.menuItemDebugLogLevelWarning.Click += new System.EventHandler(this.MenuItemDebugLogLevelWarningClick);
            // 
            // menuItemDebugLogLevelError
            // 
            this.menuItemDebugLogLevelError.Name = "menuItemDebugLogLevelError";
            this.menuItemDebugLogLevelError.Size = new System.Drawing.Size(119, 22);
            this.menuItemDebugLogLevelError.Text = "Error";
            this.menuItemDebugLogLevelError.Click += new System.EventHandler(this.MenuItemDebugLogLevelErrorClick);
            // 
            // menuItemDebugCrash
            // 
            this.menuItemDebugCrash.Name = "menuItemDebugCrash";
            this.menuItemDebugCrash.Size = new System.Drawing.Size(104, 22);
            this.menuItemDebugCrash.Text = "Crash";
            this.menuItemDebugCrash.Click += new System.EventHandler(this.MenuItemDebugCrashClick);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemHelpViewHelp,
            this.menuItemHelpSendFeedback,
            this.menuItemHelpAbout});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // menuItemHelpSendFeedback
            // 
            this.menuItemHelpSendFeedback.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemHelpFeedbackProblem,
            this.menuItemHelpFeedbackFeature});
            this.menuItemHelpSendFeedback.Name = "menuItemHelpSendFeedback";
            this.menuItemHelpSendFeedback.Size = new System.Drawing.Size(180, 22);
            this.menuItemHelpSendFeedback.Text = "Send Feedback";
            // 
            // menuItemHelpFeedbackProblem
            // 
            this.menuItemHelpFeedbackProblem.Name = "menuItemHelpFeedbackProblem";
            this.menuItemHelpFeedbackProblem.Size = new System.Drawing.Size(180, 22);
            this.menuItemHelpFeedbackProblem.Text = "Report a Problem...";
            this.menuItemHelpFeedbackProblem.Click += new System.EventHandler(this.MenuItemHelpFeedbackProblemClick);
            // 
            // menuItemHelpFeedbackFeature
            // 
            this.menuItemHelpFeedbackFeature.Name = "menuItemHelpFeedbackFeature";
            this.menuItemHelpFeedbackFeature.Size = new System.Drawing.Size(180, 22);
            this.menuItemHelpFeedbackFeature.Text = "Suggest a Feature...";
            this.menuItemHelpFeedbackFeature.Click += new System.EventHandler(this.MenuItemHelpFeedbackFeatureClick);
            // 
            // menuItemHelpAbout
            // 
            this.menuItemHelpAbout.Name = "menuItemHelpAbout";
            this.menuItemHelpAbout.Size = new System.Drawing.Size(180, 22);
            this.menuItemHelpAbout.Text = "About...";
            this.menuItemHelpAbout.Click += new System.EventHandler(this.MenuItemHelpAboutClick);
            // 
            // menuItemRunning
            // 
            this.menuItemRunning.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.menuItemRunning.AutoToolTip = true;
            this.menuItemRunning.Name = "menuItemRunning";
            this.menuItemRunning.Size = new System.Drawing.Size(64, 20);
            this.menuItemRunning.Text = "Running";
            this.menuItemRunning.ToolTipText = "Running State";
            this.menuItemRunning.Click += new System.EventHandler(this.MenuItemRunningClick);
            // 
            // timer
            // 
            this.timer.Enabled = true;
            this.timer.Tick += new System.EventHandler(this.TimerTick);
            // 
            // projectControl
            // 
            this.projectControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.projectControl.Location = new System.Drawing.Point(0, 27);
            this.projectControl.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.projectControl.Name = "projectControl";
            this.projectControl.Size = new System.Drawing.Size(932, 530);
            this.projectControl.TabIndex = 0;
            // 
            // progressBar
            // 
            this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar.Location = new System.Drawing.Point(0, 24);
            this.progressBar.MarqueeAnimationSpeed = 10;
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(938, 4);
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar.TabIndex = 2;
            this.progressBar.Value = 100;
            // 
            // menuItemHelpSendViewHelp
            // 
            this.menuItemHelpViewHelp.Name = "menuItemHelpSendViewHelp";
            this.menuItemHelpViewHelp.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F1)));
            this.menuItemHelpViewHelp.Size = new System.Drawing.Size(180, 22);
            this.menuItemHelpViewHelp.Text = "View Help";
            this.menuItemHelpViewHelp.Click += new System.EventHandler(this.MenuItemHelpViewHelpClick);
            // 
            // MainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(932, 557);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.projectControl);
            this.Controls.Add(this.menuStripMainMenu);
            this.MainMenuStrip = this.menuStripMainMenu;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.Text = "HotPin";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainFormClosing);
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
        private System.Windows.Forms.ToolStripMenuItem menuItemDebugLogOpen;
        private System.Windows.Forms.ToolStripMenuItem menuItemDebugLogLevel;
        private System.Windows.Forms.ToolStripMenuItem menuItemDebugLogLevelInfo;
        private System.Windows.Forms.ToolStripMenuItem menuItemDebugLogLevelWarning;
        private System.Windows.Forms.ToolStripMenuItem menuItemDebugLogLevelError;
        private System.Windows.Forms.ToolStripSeparator separator1;
        private System.Windows.Forms.ToolStripSeparator separator2;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuItemToolsConfiguration;
        private System.Windows.Forms.ToolStripMenuItem menuItemToolsConfigurationOpen;
        private System.Windows.Forms.ToolStripMenuItem menuItemToolsConfigurationReload;
        private System.Windows.Forms.ToolStripMenuItem menuItemHelpAbout;
        private System.Windows.Forms.ToolStripMenuItem menuItemRunning;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.ToolStripMenuItem menuItemHelpSendFeedback;
        private System.Windows.Forms.ToolStripMenuItem menuItemHelpFeedbackProblem;
        private System.Windows.Forms.ToolStripMenuItem menuItemHelpFeedbackFeature;
        private System.Windows.Forms.ToolStripMenuItem menuItemDebugCrash;
        private System.Windows.Forms.ToolStripMenuItem menuItemHelpViewHelp;
    }
}

