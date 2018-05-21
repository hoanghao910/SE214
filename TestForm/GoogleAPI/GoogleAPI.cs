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

namespace TestForm.GoogleAPI
{
    public class GoogleAPI
    {
        public GoogleAPI()
        {

        }

        static string[] Scopes = { DriveService.Scope.Drive,
                           DriveService.Scope.DriveAppdata,
                           DriveService.Scope.DriveFile,
                           DriveService.Scope.DriveMetadataReadonly,
                           DriveService.Scope.DriveReadonly,
                           DriveService.Scope.DriveScripts };
        static string ApplicationName = "Drive API .NET Quickstart";
        private string googleDriveRoot = "root";
        private string googleDriveName = "Google Drive";
        private string fileTag = "fileNotFolder";
        private string defaultEmail = "13520194@gm.uit.edu.vn";
        private string emailLogin = "";

        public void setEmailLogin(string email)
        {
            emailLogin = email;
        }

        public string getEmailLogin()
        {
            return emailLogin;
        }

        public string getGoogleDriveRoot()
        {
            return googleDriveRoot;
        }

        public string getGoogleDriveName()
        {
            return googleDriveName;
        }

        public string getdefaultEmail()
        {
            return defaultEmail;
        }

        public DriveService service;
        public UserCredential credential;

        // dang nhap vao google drive
        public void loginGoogleDrive(string email)
        {
            using (var stream =
                new FileStream("client_secret.json", FileMode.Open, FileAccess.Read))
            {
                string credPath = System.Environment.GetFolderPath(
                    System.Environment.SpecialFolder.Personal);
                credPath = Path.Combine(credPath, ".credentials/account", email);

                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    string.Format("{0}", email),
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                Console.WriteLine("Credential file saved to: " + credPath);
            }

            // Create Drive API service.
            service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = System.AppDomain.CurrentDomain.FriendlyName, //ApplicationName,
            });
        }

        // lay file tu google drive
        public IList<Google.Apis.Drive.v3.Data.File> getFilesListFromGGD(string folderId, bool isFolder)
        {
            if (folderId.Equals(fileTag)) return new List<Google.Apis.Drive.v3.Data.File>();
            string qCondition = string.Format("trashed = false and '{0}' in parents", folderId);
            if (isFolder)
            {
                qCondition = string.Format("trashed = false and '{0}' in parents and mimeType = 'application/vnd.google-apps.folder'", folderId);
            }
            // Define parameters of request.
            FilesResource.ListRequest listRequest = service.Files.List();
            listRequest.Q = qCondition;
            listRequest.PageSize = 15;
            listRequest.Fields = "nextPageToken, files(mimeType, id, name, parents, modifiedTime, modifiedByMe, createdTime, md5Checksum, sharedWithMeTime)";

            // List files.
            IList<Google.Apis.Drive.v3.Data.File> files = listRequest.Execute().Files;
            return files;
        }

        // download file tu google drive
        public void downloadFile(string fileID, string path, string fileName)
        {
            //string[] array = fileID.Split('@');
            //var request = service.Files.Export(array[0], array[1]);
            var request = service.Files.Get(fileID);
            //var request = service.Files.Export(array[0], array[1]).
            var stream = new System.IO.MemoryStream();

            request.MediaDownloader.ProgressChanged +=
            (IDownloadProgress progress) =>
            {
                switch (progress.Status)
                {
                    case DownloadStatus.Downloading:
                        {
                            Console.WriteLine(progress.BytesDownloaded);
                            break;
                        }
                    case DownloadStatus.Completed:
                        {
                            FileStream file = new FileStream(path + fileName, FileMode.Create, FileAccess.Write);
                            stream.WriteTo(file);
                            file.Close();
                            stream.Close();
                            Console.WriteLine("Download complete.");
                            break;
                        }
                    case DownloadStatus.Failed:
                        {
                            Console.WriteLine("Download failed.");
                            break;
                        }
                }
            };
            request.Download(stream);
        }

        // ham upload file len google drive
        public void uploadFile(string path, string idFolder, string email)
        {
            string mineType = GetMimeType(path);
            string name = Path.GetFileName(path);
            var fileMetadata = new Google.Apis.Drive.v3.Data.File()
            {
                Name = name,
                Parents = new List<string>() { idFolder },
                MimeType = mineType
            };
            FilesResource.CreateMediaUpload request;
            using (var stream = new System.IO.FileStream(path,
                                    System.IO.FileMode.Open))
            {
                request = service.Files.Create(
                    fileMetadata, stream, mineType);
                request.Fields = "id";

                request.Upload();
            }
        }

        // ham update file len google drive
        public void updateFile(string path, string fileId)
        {
            string mineType = GetMimeType(path);
            Google.Apis.Drive.v3.Data.File file = new Google.Apis.Drive.v3.Data.File();

            FilesResource.UpdateMediaUpload request;
            using (var stream = new System.IO.FileStream(path,
                                    System.IO.FileMode.Open))
            {
                request = service.Files.Update(file, fileId, stream, mineType);
                request.Upload();
                var body = request.ResponseBody;
            }
        }

        // lay ra minetype cua file tren google drive
        private string GetMimeType(string fileName)
        {
            string mimeType = "application/unknown";
            string ext = System.IO.Path.GetExtension(fileName).ToLower();
            Microsoft.Win32.RegistryKey regKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(ext);
            if (regKey != null && regKey.GetValue("Content Type") != null)
                mimeType = regKey.GetValue("Content Type").ToString();
            return mimeType;
        }
    }
}
