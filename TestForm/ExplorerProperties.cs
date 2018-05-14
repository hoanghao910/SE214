﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MainForm
{
    public partial class ExplorerProperties : Form
    {
        private string location = "";
        private string size = "";
        private string fileOrDirectoryName = "";
        public ExplorerProperties(string _fileOrDirectoryName, string _location, string _size)
        {
            InitializeComponent();
            location = _location;
            size = _size;
            fileOrDirectoryName = _fileOrDirectoryName;
        }

        private void ExplorerProperties_Load(object sender, EventArgs e)
        {
            this.Text = fileOrDirectoryName + " Properties";
            textBoxFileName.Text = fileOrDirectoryName;
            textBoxPath.Text = location;
            toolTip.SetToolTip(textBoxPath, location);
            labelAllSizes.Text = size;
        }

        #region Button Events
        private void buttonOkay_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
    }
}
