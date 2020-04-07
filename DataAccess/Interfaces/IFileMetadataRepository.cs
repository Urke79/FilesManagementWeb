using System.Collections.Generic;
using DataAccess.EntityModels;
using Domain;

namespace DataAccess
{
    public interface IFileMetadataRepository
    {
        bool SaveUploadedFileMetadata(FileMetadataEntity fileMetadata);
        List<FileMetadata> GetAllFiles();
        bool DeleteFile(int id);
        bool RefrheshExpiryDate(int id);
        FileMetadataEntity GetFileMetadataById(int id);
        bool RenameFileMetadata(ChangeFileMetadataNameRequest data);
    }
}