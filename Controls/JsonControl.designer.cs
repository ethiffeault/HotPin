namespace HotPin.Controls
{
    partial class JsonControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(JsonControl));
            this.PanelSearch = new System.Windows.Forms.Panel();
            this.SearchButtonClose = new System.Windows.Forms.Button();
            this.SearchButtonNext = new System.Windows.Forms.Button();
            this.SearchButtonPrevious = new System.Windows.Forms.Button();
            this.SearchTxt = new System.Windows.Forms.TextBox();
            this.linterTimer = new System.Windows.Forms.Timer(this.components);
            this.scintillaPanel = new System.Windows.Forms.Panel();
            this.contextTextBox = new System.Windows.Forms.TextBox();
            this.PanelSearch.SuspendLayout();
            this.SuspendLayout();
            // 
            // PanelSearch
            // 
            this.PanelSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.PanelSearch.Controls.Add(this.SearchButtonClose);
            this.PanelSearch.Controls.Add(this.SearchButtonNext);
            this.PanelSearch.Controls.Add(this.SearchButtonPrevious);
            this.PanelSearch.Controls.Add(this.SearchTxt);
            this.PanelSearch.Location = new System.Drawing.Point(548, 3);
            this.PanelSearch.Name = "PanelSearch";
            this.PanelSearch.Size = new System.Drawing.Size(234, 34);
            this.PanelSearch.TabIndex = 0;
            this.PanelSearch.Visible = false;
            // 
            // SearchButtonClose
            // 
            this.SearchButtonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SearchButtonClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SearchButtonClose.ForeColor = System.Drawing.Color.White;
            this.SearchButtonClose.Image = ((System.Drawing.Image)(resources.GetObject("SearchButtonClose.Image")));
            this.SearchButtonClose.Location = new System.Drawing.Point(206, 2);
            this.SearchButtonClose.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.SearchButtonClose.Name = "SearchButtonClose";
            this.SearchButtonClose.Size = new System.Drawing.Size(25, 30);
            this.SearchButtonClose.TabIndex = 11;
            this.SearchButtonClose.Tag = "Close (Esc)";
            this.SearchButtonClose.UseVisualStyleBackColor = true;
            this.SearchButtonClose.Click += new System.EventHandler(this.SearchButtonClose_Click);
            // 
            // SearchButtonNext
            // 
            this.SearchButtonNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SearchButtonNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SearchButtonNext.ForeColor = System.Drawing.Color.White;
            this.SearchButtonNext.Image = ((System.Drawing.Image)(resources.GetObject("SearchButtonNext.Image")));
            this.SearchButtonNext.Location = new System.Drawing.Point(175, 2);
            this.SearchButtonNext.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.SearchButtonNext.Name = "SearchButtonNext";
            this.SearchButtonNext.Size = new System.Drawing.Size(25, 30);
            this.SearchButtonNext.TabIndex = 10;
            this.SearchButtonNext.Tag = "Find next (Enter)";
            this.SearchButtonNext.UseVisualStyleBackColor = true;
            this.SearchButtonNext.Click += new System.EventHandler(this.SearchButtonNext_Click);
            // 
            // SearchButtonPrevious
            // 
            this.SearchButtonPrevious.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SearchButtonPrevious.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SearchButtonPrevious.ForeColor = System.Drawing.Color.White;
            this.SearchButtonPrevious.Image = ((System.Drawing.Image)(resources.GetObject("SearchButtonPrevious.Image")));
            this.SearchButtonPrevious.Location = new System.Drawing.Point(147, 1);
            this.SearchButtonPrevious.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.SearchButtonPrevious.Name = "SearchButtonPrevious";
            this.SearchButtonPrevious.Size = new System.Drawing.Size(25, 30);
            this.SearchButtonPrevious.TabIndex = 9;
            this.SearchButtonPrevious.Tag = "Find previous (Shift+Enter)";
            this.SearchButtonPrevious.UseVisualStyleBackColor = true;
            this.SearchButtonPrevious.Click += new System.EventHandler(this.SearchButtonPrevious_Click);
            // 
            // SearchTxt
            // 
            this.SearchTxt.Location = new System.Drawing.Point(3, 7);
            this.SearchTxt.Name = "SearchTxt";
            this.SearchTxt.Size = new System.Drawing.Size(138, 20);
            this.SearchTxt.TabIndex = 0;
            this.SearchTxt.TextChanged += new System.EventHandler(this.SearchTxt_TextChanged);
            this.SearchTxt.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SearchTxt_KeyDown);
            // 
            // linterTimer
            // 
            this.linterTimer.Interval = 750;
            this.linterTimer.Tick += new System.EventHandler(this.LinterTimer_Tick);
            // 
            // scintillaPanel
            // 
            this.scintillaPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.scintillaPanel.Location = new System.Drawing.Point(3, 0);
            this.scintillaPanel.Name = "scintillaPanel";
            this.scintillaPanel.Size = new System.Drawing.Size(853, 421);
            this.scintillaPanel.TabIndex = 1;
            // 
            // contextTextBox
            // 
            this.contextTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.contextTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.contextTextBox.Location = new System.Drawing.Point(3, 408);
            this.contextTextBox.Name = "contextTextBox";
            this.contextTextBox.ReadOnly = true;
            this.contextTextBox.Size = new System.Drawing.Size(853, 13);
            this.contextTextBox.TabIndex = 2;
            this.contextTextBox.Click += new System.EventHandler(this.ContextTextBox_Click);
            // 
            // JsonControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.contextTextBox);
            this.Controls.Add(this.PanelSearch);
            this.Controls.Add(this.scintillaPanel);
            this.Name = "JsonControl";
            this.Size = new System.Drawing.Size(856, 424);
            this.Load += new System.EventHandler(this.JsonControl_Load);
            this.PanelSearch.ResumeLayout(false);
            this.PanelSearch.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel PanelSearch;
        private System.Windows.Forms.TextBox SearchTxt;
        private System.Windows.Forms.Button SearchButtonClose;
        private System.Windows.Forms.Button SearchButtonNext;
        private System.Windows.Forms.Button SearchButtonPrevious;
        private System.Windows.Forms.Timer linterTimer;
        private System.Windows.Forms.Panel scintillaPanel;
        private System.Windows.Forms.TextBox contextTextBox;
    }
}
