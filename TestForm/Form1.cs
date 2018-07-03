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
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Drive.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System.Threading;
using System.Collections;
using System.Security.Cryptography;
using Google.Apis.Upload;
using Google.Apis.Download;
using TestForm.GoogleAPI;
using TestForm.Dropbox;
using System.Threading.Tasks;
using Dropbox.Api;

namespace MainForm
{
    public partial class Form1 : Form
    {
        public string[] filesName;
        public string parentFolderId = "";
        private static Semaphore _pool = new Semaphore(0, 1);
        private GoogleAPI googleAPI = new GoogleAPI();
        private DropboxAPI dropboxAPI = new DropboxAPI();
        private DropboxClient dbx;

        public Form1()
        {
            InitializeComponent();
        }

        private string path = "";
        public DriveService service;
        public UserCredential credential;
        private string pathSelectedPath = "";

        private void Form1_Load(object sender, EventArgs e)
        {
            runDOS();

            dbx = new DropboxClient("Uy12UHa2IsAAAAAAAAAAaVCDOf-IPuQGbefknfPS8ADzoXkcctW9Jv1jnGKWi8Nk");
            CreateIfMissing();
            TreeNode rootC = new TreeNode(@"C:\");
            TreeNode rootD = new TreeNode(@"D:\");
            TreeNode rootZ = new TreeNode(@"Z:\", 4, 4);
            TreeNode rootY = new TreeNode(@"Y:\", 5, 5);
            rootZ.Tag = googleAPI.getGoogleDriveRoot();
            rootY.Tag = dropboxAPI.getDropboxRoot();
            treeView.Nodes.Add(rootC);
            treeView.Nodes.Add(rootD);
            treeView.Nodes.Add(rootZ);
            treeView.Nodes.Add(rootY);
            FillChildNodes(rootC);
            FillChildNodes(rootD);
            FillChildNodes(rootZ);
            FillChildNodes(rootY);

            //đọc file gd.txt, nếu có email thì login còn không thì thôi
            checkFile();
        }

        // kiem tra da co folder google drive hay chua
        private void CreateIfMissing()
        {
            string path = @"D:\GoogleDrive";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            path = @"D:\Dropbox";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        // them cac child nodes vao o dia C, D
        public void FillChildNodes(TreeNode node)
        {
            try
            {
                string path = node.FullPath;
                path = path.Replace("\\", "/");
                path = path.Replace('\\', '/');
                DirectoryInfo dirs = new DirectoryInfo(path);
                foreach (DirectoryInfo dir in dirs.GetDirectories())
                {
                    TreeNode newnode = null;
                    if (dir.Name.Equals("GoogleDrive"))
                    {
                        newnode = new TreeNode(dir.Name, 4, 4);
                        newnode.Tag = googleAPI.getGoogleDriveRoot();
                    }
                    else if (dir.Name.Equals("Dropbox"))
                    {
                        newnode = new TreeNode(dir.Name, 5, 5);
                        newnode.Tag = dropboxAPI.getDropboxRoot();
                    }
                    else
                    {
                        newnode = new TreeNode(dir.Name);
                    }
                    node.Nodes.Add(newnode);
                    newnode.Nodes.Add("*");
                    //newnode.
                }
                if (dirs.GetDirectories().Length == 0)
                    node.Nodes.Add("*");
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message.ToString());
            }
        }

        private void treeView_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            if (e.Node.Nodes[0].Text == "*" && e.Node.Tag != null)
            {
                if (isDropboxTag(e.Node.Tag.ToString()))
                {
                    e.Node.Nodes.Clear();
                    loadFileDropbox(e.Node, getDropboxPath(e.Node.FullPath));
                }
                else
                {
                    e.Node.Nodes.Clear();
                    loadFileGoogleDrive(e.Node, googleAPI.getdefaultEmail(), e.Node.Tag.ToString(), true);
                }
            }
            else if (e.Node.FirstNode == null || e.Node.FirstNode.Text == "*")
            {
                e.Node.Nodes.Clear();
                FillChildNodes(e.Node);
            }
        }

        private void treeView_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            path = "";
            pathSelectedPath = getPath(e.Node);
            if (e.Node.FirstNode != null && e.Node.FirstNode.Text == "*" && e.Node.Tag != null && e.Node.Tag.ToString().Length > 0 && !isDropboxTag(e.Node.Tag.ToString()))
            {
                e.Node.Nodes.Clear();
                FillChildNodes(e.Node);
                loadFileGoogleDrive(e.Node, googleAPI.getdefaultEmail(), e.Node.Tag.ToString(), false);
                var files = googleAPI.getFilesListFromGGD(e.Node.Tag.ToString(), false);
                return;
            }
            if (e.Node.FirstNode != null && e.Node.FirstNode.Text == "*" && e.Node.Tag != null && isDropboxTag(e.Node.Tag.ToString()))
            {
                e.Node.Nodes.Clear();
                FillChildNodes(e.Node);
                loadFileDropbox(e.Node, getDropboxPath(pathSelectedPath));
                return;
            }
            if (e.Node.Tag != null && e.Node.Tag.ToString().Length > 0 && !isDropboxTag(e.Node.Tag.ToString()))
            {
                e.Node.Nodes.Clear();
                FillChildNodes(e.Node);
                loadFileGoogleDrive(e.Node, googleAPI.getdefaultEmail(), e.Node.Tag.ToString(), false);
                var files = googleAPI.getFilesListFromGGD(e.Node.Tag.ToString(), false);
                return;
            }
            if (e.Node.Tag != null && isDropboxTag(e.Node.Tag.ToString()))
            {
                e.Node.Nodes.Clear();
                FillChildNodes(e.Node);
                loadFileDropbox(e.Node, getDropboxPath(pathSelectedPath));
                return;
            }

            listView.Items.Clear();
            getDirectories(pathSelectedPath);
            getFiles(pathSelectedPath);

        }

        // lay duong dan folder bang de quy
        private string getPath(TreeNode node)
        {
            if (node == null) return "";
            if (node.Text.Equals(@"C:\") || node.Text.Equals(@"D:\") || node.Text.Equals(@"Z:\") || node.Text.Equals(@"Y:\"))
                return node.Text + path;
            else
            {
                path = node.Text + @"\" + path;
                return getPath(node.Parent);
            }
        }

        private string getDropboxPath(string path)
        {
            string dxPath = path;
            dxPath = dxPath.Replace("\\\\", "\\");
            dxPath = dxPath.Remove(0, "Y:".Length);
            //if (dxPath == String.Empty)
            //{
            //    dxPath = "/";
            //}
            //dxPath = dxPath.Replace("\\\\", "/");
            dxPath = dxPath.Replace("\\", "/");
            Console.WriteLine("=========" + dxPath);

            return dxPath;
        }

        private bool isDropboxTag(string tag)
        {
            if (tag.Contains("/") || tag == dropboxAPI.getDropboxName())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // lay danh sach folder
        private void getDirectories(string path)
        {
            path = path.Replace("\\", "/");
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

        // lay danh sach file
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

        private void emailgoogledrive_Click(object sender, EventArgs e)
        {
            TestForm.GoogleDrive googleDriveForm = new TestForm.GoogleDrive();
            googleDriveForm.ShowDialog();
        }

        // show files dropbox
        private async void loadFileDropbox(TreeNode node, string folder)
        {
            var files = await dropboxAPI.ListItemInFolder(dbx, folder);
            listView.Items.Clear();
            foreach (var item in files.Entries.Where(i => i.IsFolder))
            {
                var newnode = new TreeNode(item.Name, 0, 0);
                newnode.Tag = dropboxAPI.getDropboxName();
                node.Nodes.Add(newnode);
                newnode.Nodes.Add("*");
            }
            node.Expand();
            foreach (var item in files.Entries.Where(i => i.IsFolder))
            {
                var listViewItem = new ListViewItem(item.Name, 0);
                listView.Items.Add(listViewItem);
            }
            foreach (var item in files.Entries.Where(i => i.IsFile))
            {
                var listViewItem = new ListViewItem(item.Name, 1);
                listViewItem.Tag = getDropboxPath(node.FullPath);
                listView.Items.Add(listViewItem);
            }
        }
        private void loadFileGoogleDrive(TreeNode node, string email, string folderId, bool isFolder)
        {
            //if (TestForm.GoogleDrive.getEmail() != "")
            //{
            //    googleAPI.loginGoogleDrive(TestForm.GoogleDrive.getEmail());
            //}            
            var files = googleAPI.getFilesListFromGGD(folderId, isFolder);
            Console.WriteLine("Files:");
            files.Where(x => x.MimeType.Contains("folder")).ToList();
            if (files != null && files.Count > 0)
            {
                foreach (var file in files)
                {
                    Console.WriteLine("{0} ({1})", file.Name, file.Id);
                }
            }
            else
            {
                Console.WriteLine("No files found.");
            }
            Console.Read();
            if (isFolder)
            {
                TreeNode newnode;

                foreach (var item in files)
                {
                    if (item.MimeType.Contains("folder"))
                    {
                        newnode = new TreeNode(item.Name, 0, 0);
                        newnode.Tag = item.Id;
                        node.Nodes.Add(newnode);
                        newnode.Nodes.Add("*");
                    }
                }
            }
            else
            {
                listView.Items.Clear();
                foreach (var item in files)
                {
                    if (item.MimeType.Contains("folder"))
                    {
                        string[] row = { item.Name, item.ModifiedTime.ToString() };
                        var listViewItem = new ListViewItem(row, 0);
                        listView.Tag = item.Id + "@" + item.MimeType;
                        listView.Items.Add(listViewItem);
                    }
                }
                foreach (var item in files)
                {
                    if (!item.MimeType.Contains("folder"))
                    {
                        string[] row = { item.Name, item.ModifiedTime.ToString() };
                        var listViewItem = new ListViewItem(row, 1);
                        listViewItem.Tag = item.Id;
                        listViewItem.Tag = item.Id + "@" + item.MimeType;
                        listView.Items.Add(listViewItem);
                    }
                }
            }
        }

        private void toolStripMenuItemProperties_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;
            openFileDialog1.Multiselect = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                filesName = openFileDialog1.FileNames;
                parentFolderId = treeView.SelectedNode.Tag.ToString();
                Thread threadUploadFiles = new Thread(threadUploadFile);
                threadUploadFiles.Start();
                //_pool.WaitOne(); // cho thread chay xong, sau khi thread release
                //MessageBox.Show("Upload success!!");
            }
        }

        // tao thread de upload file len google drive
        public async void threadUploadFile()
        {
            // Check upload dropbox
            if (isDropboxTag(parentFolderId))
            {
                foreach (String file in filesName)
                {
                    FileInfo fileInfo = new FileInfo(file);
                    await dropboxAPI.Upload(dbx, getDropboxPath(pathSelectedPath), fileInfo.Name, fileInfo.FullName);
                    if (listView.InvokeRequired)
                    {
                        listView.Invoke(new MethodInvoker(delegate
                        {
                            string[] row = { Path.GetFileName(Path.GetFileName(file)),
                                fileInfo.LastWriteTime.ToString() };
                            var listViewItem = new ListViewItem(row, 1);
                            listView.Items.Add(listViewItem);
                        }));
                    }
                    return;

                }
            }

            var files = googleAPI.getFilesListFromGGD(parentFolderId, false);
            foreach (String file in filesName)
            {
                FileInfo fileInfo = new FileInfo(file);
                var aFile = files.Where(x => x.Name.Equals(Path.GetFileName(file))).FirstOrDefault();
                if (aFile == null)
                {
                    googleAPI.uploadFile(file, parentFolderId, googleAPI.getdefaultEmail());
                    if (listView.InvokeRequired && aFile == null)
                    {
                        listView.Invoke(new MethodInvoker(delegate
                        {
                            string[] row = { Path.GetFileName(Path.GetFileName(file)),
                                fileInfo.LastWriteTime.ToString() };
                            var listViewItem = new ListViewItem(row, 1);
                            listView.Items.Add(listViewItem);
                        }));
                    }
                }
                else
                {
                    string md5 = CalculateMD5(file);
                    //không phải folder, cùng tên file, ngày của file trên đĩa mới hơn ngày trên goolge drive
                    //và checksum md5 khác nhau (nghĩa là file có thay đổi)
                    var fileUpdate = files.Where(x => !x.MimeType.Contains("folder") &&
                            x.Name.Equals(Path.GetFileName(file)) &&
                            fileInfo.LastWriteTime > x.ModifiedTime &&
                            !x.Md5Checksum.Equals(md5)).FirstOrDefault();
                    if (fileUpdate != null)
                    {
                        googleAPI.updateFile(file, aFile.Id);
                        if (listView.InvokeRequired && aFile == null)
                        {
                            listView.Invoke(new MethodInvoker(delegate
                            {
                                string[] row = { Path.GetFileName(Path.GetFileName(file)),
                                fileInfo.LastWriteTime.ToString() };
                                var listViewItem = new ListViewItem(row, 1);
                                listView.Items.Add(listViewItem);
                            }));
                        }
                    }
                }
            }
            //_pool.Release();
        }

        // checksum
        private string CalculateMD5(string filename)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = System.IO.File.OpenRead(filename))
                {
                    var hash = md5.ComputeHash(stream);
                    return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
                }
            }
        }

        public List<string> listString = new List<string>();

        // download file tu google drive
        private void downloadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(pathSelectedPath))
            {
                Directory.CreateDirectory(pathSelectedPath);
            }

            foreach (ListViewItem item in listView.SelectedItems)
            {
                string tag = item.Tag.ToString();

                string name = item.Text;

                listString.Add(tag + "@" + name);
            }
            Thread threadUploadFiles = new Thread(threadDownloadFile);
            threadUploadFiles.Start();
        }

        public async void threadDownloadFile()
        {
            string[] array;

            foreach (var item in listString)
            {
                array = item.Split('@');
                if (array[0] != null)
                {
                    if (isDropboxTag(array[0]))
                    {
                        await dropboxAPI.Download(dbx, array[0], array[1]);
                    }
                    else
                    {
                        googleAPI.downloadFile(array[0], pathSelectedPath, array[2]);
                    }
                    Array.Clear(array, 0, array.Length);
                }
            }
        }

        private void listView_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && treeView.SelectedNode.Tag != null)
            {
                contextMenuStrip.Show(Cursor.Position);
            }
        }

        private void runDOS()
        {
            string strCmdText;
            strCmdText = @"/C subst Z: D:\GoogleDrive";
            System.Diagnostics.Process.Start("CMD.exe", strCmdText);
            strCmdText = @"/C subst Z: D:\Dropbox";
            System.Diagnostics.Process.Start("CMD.exe", strCmdText);
        }

        private void checkFile()
        {
            string path = @".\gd.txt";
            if (System.IO.File.Exists(path))
            {
                string[] lines = System.IO.File.ReadAllLines(path);
                foreach (string line in lines)
                {
                    googleAPI.loginGoogleDrive(line);
                }
            }
        }
    }
}
