using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiasShared.Classes.Dto
{
    public class FileMetaDataDto
    {
        public string FileName;
        public string FileType;
        public byte[] FileData { get; set; }
    }
}
