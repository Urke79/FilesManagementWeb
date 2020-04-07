using DataAccess.EntityModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class FileMetadataDbContext : DbContext
    {
        public FileMetadataDbContext() : base("FileMetadataDbConnectionString")
        {
           // Database.Log = sql => Debug.Write(sql);
        }

        public DbSet<FileMetadataEntity> UploadedFileInfo { get; set; }
    }
}
