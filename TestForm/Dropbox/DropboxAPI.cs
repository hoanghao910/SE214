using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nemiro.OAuth;
using Nemiro.OAuth.LoginForms;
using System.Threading.Tasks;
using Dropbox.Api;
using Dropbox.Api.Files;
using System.IO;

namespace TestForm.Dropbox
{
    class DropboxAPI
    {
        private string dropboxRoot = "/home";
        private string dropboxName = "Dropbox";
        private string dropboxTag = "dropbox";

        public string getDropboxRoot()
        {
            return dropboxRoot;
        }

        public string getDropboxName()
        {
            return dropboxName;
        }

        public string getDropboxTag()
        {
            return dropboxTag;
        }

        public async Task<ListFolderResult> ListRootFolder(DropboxClient dbx)
        {
            var result = await dbx.Files.ListFolderAsync(string.Empty);

            return result;
        }

        public async Task<ListFolderResult> ListItemInFolder(DropboxClient dbx, string folder)
        {
            if (folder == dropboxRoot || folder == dropboxName || folder == "/")
            {
                folder = string.Empty;
            }
            var result = await dbx.Files.ListFolderAsync(folder);

            return result;
        }

        public async Task Download(DropboxClient dbx, string folder, string file)
        {
            using (var response = await dbx.Files.DownloadAsync(folder + "/" + file))
            {
                await response.GetContentAsStringAsync();
                MessageBox.Show("Download Successful!!!");
            }
        }

        public async Task Upload(DropboxClient dbx, string folder, string file, string content)
        {
            using (var mem = new MemoryStream(Encoding.UTF8.GetBytes(content)))
            {
                var updated = await dbx.Files.UploadAsync(
                    folder + "/" + file,
                    WriteMode.Overwrite.Instance,
                    body: mem);
                MessageBox.Show("Upload Successful!!!");
            }
        }
    }
}
