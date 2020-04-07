using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.EntityModels;
using Domain;
using AutoMapper;
using System.Collections;
using System.Data.Entity;
using System.IO;
using System.Configuration;

namespace DataAccess
{
    public class FileMetadataRepository : IFileMetadataRepository
    {
        private IMapper _mapper;
        private FileMetadataDbContext _context;

        public FileMetadataRepository(IMapper mapper, FileMetadataDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public List<FileMetadata> GetAllFiles()
        {
            var files = _context.UploadedFileInfo.ToList();

            return _mapper.Map<List<FileMetadata>>(files);
        }

        public FileMetadataEntity GetFileMetadataById(int id)
        {
            var fileMetadata = _context.UploadedFileInfo.Find(id);
            
            return fileMetadata;
        }

        public bool SaveUploadedFileMetadata(FileMetadataEntity fileMetadata)
        {
            var isAdded = false;

            try
            {
                _context.UploadedFileInfo.Add(fileMetadata);
                isAdded = Save();
            }
            catch (Exception e)
            {
                // TO DO
            }

            return isAdded;
        }

        public bool DeleteFile(int id)
        {
            var isDeleted = false;
            var fileMetadata = _context.UploadedFileInfo.Find(id);

            try
            {
                _context.UploadedFileInfo.Remove(fileMetadata);
                isDeleted = Save();
                // Delete file from file system
                File.Delete(fileMetadata.Url);
            }
            catch (Exception ex)
            {

                // TO DO
            }

            return isDeleted;
        }

        public bool RefrheshExpiryDate(int id)
        {   
                     
            var isUpdated = false;
            var fileMetadata = _context.UploadedFileInfo.Find(id);


            if (fileMetadata != null)
            {
                fileMetadata.Expires = DateTime.Now.AddDays(90);

                try
                {
                    _context.Entry(fileMetadata).State = EntityState.Modified;
                    isUpdated = Save();
                }
                catch (Exception e)
                {
                    // TO DO
                }
            }

            return isUpdated;
        }

        public bool RenameFileMetadata(ChangeFileMetadataNameRequest data)
        {
            var isUpdated = false;
            var fileMetadata = _context.UploadedFileInfo.Find(data.Id);
            var oldName = fileMetadata.Url;
            var originalDirectory = ConfigurationManager.AppSettings["uploadDestination"];
            var newFileLocation = string.Format("{0}\\{1}", originalDirectory, data.NewName);

            if (fileMetadata != null)
            {
                // TO DO - Check if file extension is correct
                fileMetadata.FileName = data.NewName;
                fileMetadata.Url = newFileLocation;

                try
                {
                    _context.Entry(fileMetadata).State = EntityState.Modified;
                    isUpdated = Save();

                    RenameFileInFileSystem(oldName, newFileLocation);
                }
                catch (Exception e)
                {
                    // TO DO
                }
            }

            return isUpdated;
        }

        private void RenameFileInFileSystem(string oldName, string newFileLocation)
        {
            FileInfo fileInfo = new FileInfo(oldName);

            if (fileInfo.Exists)
            {
                fileInfo.MoveTo(newFileLocation);
            }
        }

        private bool Save()
        {
            return _context.SaveChanges() > 0;
        }
    }
}
