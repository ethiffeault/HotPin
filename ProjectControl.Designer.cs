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
            this.treeView = new HotPin.Controls.TreeView();
            this.jsonControl = new HotPin.Controls.JsonControl();
            this.SuspendLayout();
            // 
            // treeView
            // 
            this.treeView.AllowDrop = true;
            this.treeView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.treeView.Location = new System.Drawing.Point(3, 3);
            this.treeView.Name = "treeView";
            this.treeView.Size = new System.Drawing.Size(267, 334);
            this.treeView.TabIndex = 0;
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
            // ProjectControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.jsonControl);
            this.Controls.Add(this.treeView);
            this.DoubleBuffered = true;
            this.Name = "ProjectControl";
            this.Size = new System.Drawing.Size(641, 343);
            this.ResumeLayout(false);

        }

        #endregion

        private HotPin.Controls.TreeView treeView;
        private Controls.JsonControl jsonControl;
    }
}
