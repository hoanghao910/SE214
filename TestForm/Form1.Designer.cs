using System;

namespace MainForm
{
    partial class Form1
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("C");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("D");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Google Drive");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Dropbox");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("One Drive");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("Cloud", new System.Windows.Forms.TreeNode[] {
            treeNode3,
            treeNode4,
            treeNode5});
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.treeView = new System.Windows.Forms.TreeView();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemProperties = new System.Windows.Forms.ToolStripMenuItem();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.listView = new System.Windows.Forms.ListView();
            this.imageListIcon = new System.Windows.Forms.ImageList(this.components);
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeView
            // 
            this.treeView.ContextMenuStrip = this.contextMenuStrip;
            this.treeView.ImageKey = "open.png";
            this.treeView.ImageList = this.imageList;
            this.treeView.Location = new System.Drawing.Point(-1, -1);
            this.treeView.Name = "treeView";
            treeNode1.Name = "NodeC";
            treeNode1.Text = "C";
            treeNode2.Name = "NodeD";
            treeNode2.Text = "D";
            treeNode3.Name = "NodeGGD";
            treeNode3.Text = "Google Drive";
            treeNode4.Name = "NodeDB";
            treeNode4.Text = "Dropbox";
            treeNode5.Name = "NodeOD";
            treeNode5.Text = "One Drive";
            treeNode6.Name = "NodeCloud";
            treeNode6.Text = "Cloud";
            this.treeView.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode6});
            this.treeView.SelectedImageIndex = 0;
            this.treeView.Size = new System.Drawing.Size(260, 389);
            this.treeView.TabIndex = 0;
            this.treeView.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView_NodeMouseClick);
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemCopy,
            this.toolStripMenuItemPaste,
            this.toolStripMenuItemDelete,
            this.toolStripMenuItemProperties});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(128, 92);
            // 
            // toolStripMenuItemCopy
            // 
            this.toolStripMenuItemCopy.Name = "toolStripMenuItemCopy";
            this.toolStripMenuItemCopy.Size = new System.Drawing.Size(127, 22);
            this.toolStripMenuItemCopy.Text = "Copy";
            this.toolStripMenuItemCopy.Click += new System.EventHandler(this.toolStripMenuItemCopy_Click);
            // 
            // toolStripMenuItemPaste
            // 
            this.toolStripMenuItemPaste.Name = "toolStripMenuItemPaste";
            this.toolStripMenuItemPaste.Size = new System.Drawing.Size(127, 22);
            this.toolStripMenuItemPaste.Text = "Paste";
            this.toolStripMenuItemPaste.Click += new System.EventHandler(this.toolStripMenuItemPaste_Click);
            // 
            // toolStripMenuItemDelete
            // 
            this.toolStripMenuItemDelete.Name = "toolStripMenuItemDelete";
            this.toolStripMenuItemDelete.Size = new System.Drawing.Size(127, 22);
            this.toolStripMenuItemDelete.Text = "Delete";
            this.toolStripMenuItemDelete.Click += new System.EventHandler(this.toolStripMenuItemDelete_Click);
            // 
            // toolStripMenuItemProperties
            // 
            this.toolStripMenuItemProperties.Name = "toolStripMenuItemProperties";
            this.toolStripMenuItemProperties.Size = new System.Drawing.Size(127, 22);
            this.toolStripMenuItemProperties.Text = "Properties";
            this.toolStripMenuItemProperties.Click += new System.EventHandler(this.toolStripMenuItemProperties_Click);
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "new-folder.png");
            this.imageList.Images.SetKeyName(1, "new-file.png");
            this.imageList.Images.SetKeyName(2, "xml-file.png");
            this.imageList.Images.SetKeyName(3, "txt-file.png");
            // 
            // listView
            // 
            this.listView.Location = new System.Drawing.Point(253, -1);
            this.listView.Name = "listView";
            this.listView.Size = new System.Drawing.Size(443, 389);
            this.listView.SmallImageList = this.imageListIcon;
            this.listView.StateImageList = this.imageListIcon;
            this.listView.TabIndex = 1;
            this.listView.UseCompatibleStateImageBehavior = false;
            this.listView.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            // 
            // imageListIcon
            // 
            this.imageListIcon.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListIcon.ImageStream")));
            this.imageListIcon.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListIcon.Images.SetKeyName(0, "disk.png");
            this.imageListIcon.Images.SetKeyName(1, "folder__plus.png");
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(699, 387);
            this.Controls.Add(this.listView);
            this.Controls.Add(this.treeView);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private void treeView_Click(object sender, EventArgs e)
        {
           
        }

        #endregion

        private System.Windows.Forms.TreeView treeView;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemCopy;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemPaste;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemDelete;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemProperties;
        private System.Windows.Forms.ListView listView;
        private System.Windows.Forms.ImageList imageListIcon;
    }
}

