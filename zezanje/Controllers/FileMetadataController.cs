using DataAccess;
using DataAccess.EntityModels;
using Domain;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Web;

namespace FileControl.Controllers
{
    public class FileMetadataController : ApiController
    {
        private IFileMetadataRepository _repository;

        public FileMetadataController(IFileMetadataRepository fileMetadataRepository)
        {
            _repository = fileMetadataRepository;
        }
        public List<FileMetadata> ShowFiles()
        {
            var allFiles = _repository.GetAllFiles();

            return allFiles;
        }

        [Route("api/FileMetadata/SaveUploadedFile")]
        public bool SaveUploadedFile()
        {
            bool isSavedSuccessfully = true;

            try
            {
                var httpRequest = HttpContext.Current.Request;
                if (httpRequest.Files.Count < 1)
                {
                    isSavedSuccessfully = false;
                }

                foreach (string file in httpRequest.Files)
                {
                    var postedFile = httpRequest.Files[file];
                    if (postedFile != null && postedFile.ContentLength > 0)
                    {
                        string path;

                        SaveFileToUploadFolder(postedFile, out path);
                        SaveFileMetadataToDb(path);
                    }
                }
            }
            catch (Exception ex)
            {
                isSavedSuccessfully = false;
            }

            return isSavedSuccessfully;
        }

        [HttpDelete]
        public bool DeleteFile(int id)
        {
            var isDeleted = _repository.DeleteFile(id);
            return isDeleted;
        }

        [HttpPut]
        public bool RefrheshExpiryDate(int id)
        {
            var isUpdated = _repository.RefrheshExpiryDate(id);

            return isUpdated;
        }

        [HttpPut]
        public bool UpdateFileMetadata([FromBody] ChangeFileMetadataNameRequest data)
        {
            var isUpdated = _repository.RenameFileMetadata(data);
            return isUpdated;
        }

        [HttpGet]
        public HttpResponseMessage DownloadFile(int id)
        {
            var result = new HttpResponseMessage(HttpStatusCode.OK);

            var fileMetadata = _repository.GetFileMetadataById(id);
            var fileBytes = File.ReadAllBytes(fileMetadata.Url);

            var memoryStream = new MemoryStream(fileBytes);
            result.Content = new StreamContent(memoryStream);

            result.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
            result.Content.Headers.ContentDisposition.FileName = fileMetadata.FileName;
            result.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");

            return result;
        }

        private void SaveFileToUploadFolder(HttpPostedFile postedFile, out string path)
        {
            // get upload destination from app configuration
            var originalDirectory = ConfigurationManager.AppSettings["uploadDestination"];

            // Create upload folder if it doesn't exist
            bool isExistsing = Directory.Exists(originalDirectory);
            if (!isExistsing)
                Directory.CreateDirectory(originalDirectory);

            // save file to upload destination folder
            path = string.Format("{0}\\{1}", originalDirectory, postedFile.FileName);
            postedFile.SaveAs(path);
        }

        private void SaveFileMetadataToDb(string path)
        {
            var fileInfo = new FileInfo(path);

            var fileMetadataEntity = new FileMetadataEntity
            {
                FileName = fileInfo.Name,
                DateUploaded = fileInfo.CreationTimeUtc,
                Size = BytesToString(fileInfo.Length),
                Expires = fileInfo.CreationTimeUtc.AddDays(90),
                Url = fileInfo.FullName
            };

            _repository.SaveUploadedFileMetadata(fileMetadataEntity);
        }

        private static string BytesToString(long byteCount)
        {
            string[] suf = { "B", "KB", "MB", "GB", "TB", "PB", "EB" }; 
            if (byteCount == 0)
                return "0" + suf[0];
            long bytes = Math.Abs(byteCount);
            int place = Convert.ToInt32(Math.Floor(Math.Log(bytes, 1024)));
            double num = Math.Round(bytes / Math.Pow(1024, place), 1);
            return (Math.Sign(byteCount) * num).ToString() + suf[place];
        }
    }
}
