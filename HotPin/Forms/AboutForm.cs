﻿using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace HotPin
{
    public partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();
            pictureBox.BackgroundImage = Application.Resources.HotPin;
            textBox.Text = $"HotPin{Environment.NewLine}Version {Application.Version}{Environment.NewLine}{RuntimeInformation.FrameworkDescription}";
            textBox.SelectionStart = 0;
        }

        private void ButtonLicenceClick(object sender, EventArgs e)
        {
            Utils.StartProcess( Application.Licence );
        }
    }
}
