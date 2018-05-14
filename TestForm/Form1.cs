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

        public List<string> listFile = new List<string>();

        DirectoryInfo directoryInfoC = new DirectoryInfo(@"C:\");
        DirectoryInfo directoryInfoD = new DirectoryInfo(@"D:\");
        private void Form1_Load(object sender, EventArgs e)
        {
            loadDirectories(directoryInfoC, treeView.Nodes[0]);
            loadDirectories(directoryInfoD, treeView.Nodes[1]);
            //if (Directory.Exists(@"C:\"))
            //{
            //    try
            //    {
            //        DirectoryInfo[] directories = directoryInfoC.GetDirectories();

            //        foreach (FileInfo file in directoryInfoC.GetFiles())
            //        {
            //            if (file.Exists)
            //            {
            //                TreeNode nodes = treeView.Nodes[0].Nodes.Add(file.Name);
            //                SetImageExtension(file.Name, nodes);
            //            }
            //        }

            //        if (directories.Length > 0)
            //        {
            //            foreach (DirectoryInfo directory in directories)
            //            {
            //                TreeNode node = treeView.Nodes[0].Nodes.Add(directory.Name);
            //                node.ImageIndex = node.SelectedImageIndex = 0;
            //                foreach (FileInfo file in directory.GetFiles())
            //                {
            //                    if (file.Exists)
            //                    {
            //                        TreeNode nodes = treeView.Nodes[0].Nodes[node.Index].Nodes.Add(file.Name);
            //                        SetImageExtension(file.Name, nodes);
            //                    }
            //                }
            //            }
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show(ex.Message);
            //    }
            //}
            //if (Directory.Exists(@"D:\"))
            //{
            //    try
            //    {
            //        DirectoryInfo[] directories = directoryInfoD.GetDirectories();

            //        foreach (FileInfo file in directoryInfoD.GetFiles())
            //        {
            //            if (file.Exists)
            //            {
            //                TreeNode nodes = treeView.Nodes[1].Nodes.Add(file.Name);
            //                SetImageExtension(file.Name, nodes);
            //            }
            //        }

            //        if (directories.Length > 0)
            //        {
            //            foreach (DirectoryInfo directory in directories)
            //            {
            //                TreeNode node = treeView.Nodes[1].Nodes.Add(directory.Name);
            //                node.ImageIndex = node.SelectedImageIndex = 0;
            //                foreach (FileInfo file in directory.GetFiles())
            //                {
            //                    if (file.Exists)
            //                    {
            //                        TreeNode nodes = treeView.Nodes[1].Nodes[node.Index].Nodes.Add(file.Name);
            //                        SetImageExtension(file.Name, nodes);
            //                    }
            //                }
            //            }
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show(ex.Message);
            //    }
            //}

            //listViewLoad("");
        }

        private void toolStripMenuItemCopy_Click(object sender, EventArgs e)
        {
            if (treeView.SelectedNode != null)
            {
                try
                {
                    DirectoryInfo[] directories = directoryInfoC.GetDirectories();
                    foreach (FileInfo file in directoryInfoC.GetFiles())
                    {
                        if (file.Exists && file.Name == treeView.SelectedNode.Text)
                        {
                            StringCollection filePath = new StringCollection();
                            filePath.Add(file.FullName);
                            Clipboard.SetFileDropList(filePath);
                        }
                    }

                    if (directories.Length > 0)
                    {
                        foreach (DirectoryInfo directory in directories)
                        {
                            if (directory.Name == treeView.SelectedNode.Text)
                            {
                                StringCollection folderPath = new StringCollection();
                                folderPath.Add(directory.FullName);
                                Clipboard.SetFileDropList(folderPath);
                            }

                            foreach (FileInfo file in directory.GetFiles())
                            {
                                if (file.Exists && file.Name == treeView.SelectedNode.Text)
                                {
                                    StringCollection filePath = new StringCollection();
                                    filePath.Add(file.FullName);
                                    Clipboard.SetFileDropList(filePath);
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void toolStripMenuItemPaste_Click(object sender, EventArgs e)
        {
            if (treeView.SelectedNode != null)
            {
                bool copy = false;
                try
                {
                    DirectoryInfo[] directories = directoryInfoC.GetDirectories();
                    if (directories.Length > 0)
                    {
                        foreach (DirectoryInfo directory in directories)
                        {
                            if (directory.Name == treeView.SelectedNode.Text && Clipboard.ContainsFileDropList())
                            {
                                foreach (string file in Clipboard.GetFileDropList())
                                {
                                    string targetDir = directoryInfoC.FullName + @"\" + directory.Name;
                                    File.Copy(Path.Combine(file.Replace(Path.GetFileName(file), ""), Path.GetFileName(file)), Path.Combine(targetDir, Path.GetFileName(file)), true);
                                }
                                copy = true;
                            }
                        }
                    }

                    if (copy)
                    {
                        foreach (string file in Clipboard.GetFileDropList())
                        {
                            TreeNode node = treeView.Nodes[0].Nodes[treeView.SelectedNode.Index].Nodes.Add(Path.GetFileName(file));
                            SetImageExtension(file, node);
                        }
                        copy = false;
                    }
                }
                catch (Exception ex)
                {
                    copy = false;
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void toolStripMenuItemDelete_Click(object sender, EventArgs e)
        {
            if (treeView.SelectedNode != null)
            {
                bool deleted = false;
                try
                {
                    DirectoryInfo[] directories = directoryInfoC.GetDirectories();
                    foreach (FileInfo file in directoryInfoC.GetFiles())
                    {
                        if (file.Exists && file.Name == treeView.SelectedNode.Text)
                        {
                            file.Delete();
                            deleted = true;
                        }
                    }

                    if (directories.Length > 0)
                    {
                        foreach (DirectoryInfo directory in directories)
                        {
                            foreach (FileInfo file in directory.GetFiles())
                            {
                                if (file.Exists && file.Name == treeView.SelectedNode.Text)
                                {
                                    file.Delete();
                                    deleted = true;
                                }
                            }

                            if (treeView.SelectedNode.Text == directory.Name)
                            {
                                foreach (FileInfo file in directory.GetFiles())
                                {
                                    if (file.Exists)
                                        file.Delete();
                                }
                                directory.Delete();
                                deleted = true;
                            }
                        }
                    }

                    if (deleted)
                        treeView.SelectedNode.Remove();
                }
                catch (Exception ex)
                {
                    deleted = false;
                    MessageBox.Show(ex.Message);
                }
            }
        }

        ExplorerProperties properties;
        private void toolStripMenuItemProperties_Click(object sender, EventArgs e)
        {
            if (treeView.SelectedNode != null)
            {
                if (properties != null)
                    properties.Close();

                DirectoryInfo[] directories = directoryInfoC.GetDirectories();

                foreach (DirectoryInfo directory in directories)
                {
                    if (!treeView.SelectedNode.Text.Contains(".")) // Folder, not a file
                    {
                        if (directory.Name == treeView.SelectedNode.Text)
                        {
                            properties = new ExplorerProperties(directory.Name, directory.FullName, CalculateBytes(directory.GetFiles("*.*", SearchOption.AllDirectories).Sum(file => file.Length)) + " (" + directory.GetFiles("*.*", SearchOption.AllDirectories).Sum(file => file.Length).ToString() + " bytes)");
                            properties.Show();
                        }
                    }
                    else
                    {
                        foreach (FileInfo file in directoryInfoC.GetFiles())
                        {
                            if (file.Name == treeView.SelectedNode.Text)
                            {
                                properties = new ExplorerProperties(file.Name, file.FullName, CalculateBytes(file.Length) + " (" + file.Length + " bytes)");
                                properties.Show();
                                return;
                            }
                        }

                        foreach (FileInfo file in directory.GetFiles())
                        {
                            if (file.Name == treeView.SelectedNode.Text)
                            {
                                properties = new ExplorerProperties(file.Name, file.FullName, CalculateBytes(file.Length) + " (" + file.Length + " bytes)");
                                properties.Show();
                            }
                        }
                    }
                }
            }
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

        private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            listViewLoad(treeView_GetPathOfNode(e.Node));

        }

        private string treeView_GetPathOfNode(TreeNode node)
        {
            if (node.Text != "C" && node.Text != "D")
            {
                return treeView_GetPathOfNode(node.Parent) + @"\" + node.Text;
            }
            else
            {
                return node.Text + @":\";
            }

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public void listViewLoad(string Path)
        {
            //Path = @"D:\UIT\Storage_Data\nemiro.oauth.dll-master\examples\DropboxExample\Resources";
            try
            {
                if (listView != null)
                {
                    listView.Items.Clear();
                    listFile.Clear();
                }
                foreach (string item in Directory.GetFiles(Path))
                {
                    imageListIcon.Images.Add(System.Drawing.Icon.ExtractAssociatedIcon(item));
                    FileInfo fi = new FileInfo(item);
                    listFile.Add(fi.FullName);
                    listView.Items.Add(fi.Name, 0);
                }
            }
            catch (Exception e)
            {

            }
        }


        private void treeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            string path = treeView_GetPathOfNode(e.Node);
            listViewLoad(path);
            loadDirectories(new DirectoryInfo(path), e.Node);
        }

        private void loadDirectories(DirectoryInfo directoryPath, TreeNode node)
        {
            try
            {
                DirectoryInfo[] directories = directoryPath.GetDirectories();
                if (directories.Length > 0 && node.Nodes.Count <= directories.Length)
                {
                    foreach (DirectoryInfo directory in directories)
                    {
                        //show directories
                        node.Nodes.Add(directory.Name);
                        treeView.SelectedNode = node;
                        //treeView.Refresh();
                        //loadDirectoriesAndFiles(directory, nodes);
                    }
                    //FileInfo[] files = directoryPath.GetFiles();
                    //foreach (FileInfo file in files)
                    //{
                    //    //show file
                    //    if (file.Exists)
                    //    {
                    //        TreeNode nodes = node.Nodes.Add(file.Name);
                    //        SetImageExtension(file.Name, nodes);
                    //    }
                    //}
                }
                else
                {
                    return;
                }
            }
            catch (System.UnauthorizedAccessException)
            {
                //return new TreeNode("Unavailable Node");
                return;
            }
            catch (System.IO.PathTooLongException)
            {
                //return new TreeNode("Unavailable Node");
                return;
            }

        }
    }
}
