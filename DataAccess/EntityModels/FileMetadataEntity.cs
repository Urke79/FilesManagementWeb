using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.EntityModels
{
    public class FileMetadataEntity
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string Size { get; set; }
        public DateTime DateUploaded { get; set; }
        public DateTime Expires { get; set; }
        public string Url { get; set; }
    }
}
