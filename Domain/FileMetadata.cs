using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class FileMetadata
    {
        // this is to enable storing id from DB into the tr element of each row in the data table
        public int DT_RowId { get; set; }
        public string FileName { get; set; }
        public string Size { get; set; }
        public DateTime DateUploaded { get; set; }
        public int Expires { get; set; }
        public string Url { get; set; }
    }
}
