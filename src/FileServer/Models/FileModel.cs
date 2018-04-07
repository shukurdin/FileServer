using System;

namespace FileServer.Models
{
    public class FileModel
    {
        public string Name { get; set; }
        public long Length { get; set; }
        public DateTime LastModifiedDate { get; set; }
    }
}