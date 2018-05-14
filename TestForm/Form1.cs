using System;
using System.Text.RegularExpressions;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace MainForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private string path = "";

        private void Form1_Load(object sender, EventArgs e)
        {
            TreeNode rootC = new TreeNode(@"C:\");
            TreeNode rootD = new TreeNode(@"D:\");
            treeView.Nodes.Add(rootC);
            treeView.Nodes.Add(rootD);
            FillChildNodes(rootC);
            FillChildNodes(rootD);
            //treeView.Nodes[0].Expand();
        }

        #region Calculation
        private string CalculateBytes(long count)
        {
            string[] sizeNames = { " B", " KB", " MB", " GB", " TB", " PB", " EB" };
            if (count == 0)
                return "0" + sizeNames[0];
            long bytes = Math.Abs(count);
            int log = Convert.ToInt32(Math.Floor(Math.Log(bytes, 1024)));
            double number = Math.Round(bytes / Math.Pow(1024, log), 1);
            return (Math.Sign(count) * number).ToString() + sizeNames[log];
        }
        #endregion

        #region Extensions
        private void SetImageExtension(string name, TreeNode _node)
        {
            if (name.Contains("xml"))
                _node.ImageIndex = _node.SelectedImageIndex = 2;
            else if (name.Contains("txt"))
                _node.ImageIndex = _node.SelectedImageIndex = 3;
        }
        #endregion

        public void FillChildNodes(TreeNode node)
        {
            try
            {
                DirectoryInfo dirs = new DirectoryInfo(node.FullPath);
                foreach (DirectoryInfo dir in dirs.GetDirectories())
                {
                    TreeNode newnode = new TreeNode(dir.Name);
                    node.Nodes.Add(newnode);
                    newnode.Nodes.Add("*");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void treeView_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            if (e.Node.Nodes[0].Text == "*")
            {
                e.Node.Nodes.Clear();
                FillChildNodes(e.Node);
            }
        }

        private void treeView_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            //string path = e.Node.Text;
            path = "";
            string pathSelectedNode = getPath(e.Node);

            listView.Items.Clear();
            getDirectories(pathSelectedNode);
            getFiles(pathSelectedNode);
        }

        
        private string getPath(TreeNode node)
        {
            if (node == null) return "";
            if (node.Text.Equals(@"C:\") || node.Text.Equals(@"D:\"))
                return node.Text + path;
            else
            {
                path = node.Text + @"\" + path ;
                return getPath(node.Parent);
            }                
        }

        private void getDirectories(string path)
        {
            if (path.Length > 0)
            {
                DirectoryInfo dirs = new DirectoryInfo(path);
                foreach (DirectoryInfo dir in dirs.GetDirectories())
                {
                    string[] row = { dir.Name, dir.LastWriteTime.ToString() };
                    var listViewItem = new ListViewItem(row, 0);
                    listView.Items.Add(listViewItem);
                }
            }            
        }

        private void getFiles(string path)
        {
            if (path.Length > 0)
            {
                DirectoryInfo dir = new DirectoryInfo(path);
                FileInfo[] Files = dir.GetFiles("*.*");

                foreach (FileInfo file in Files)
                {
                    string[] row = { file.Name, file.LastWriteTime.ToString() };
                    var listViewItem = new ListViewItem(row, 1);
                    listView.Items.Add(listViewItem);
                }
            }            
        }
    }
}
