using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FileServer.Models;
using Microsoft.AspNetCore.Http;

namespace FileServer.Services
{
    public class FileSystemStorage
        : IFileStorage
    {
        readonly string directoryPath;

        public FileSystemStorage(string directoryPath)
        {
            if (!Directory.Exists(directoryPath))
                throw new ArgumentException(directoryPath);

            this.directoryPath = directoryPath;
        }

        public FileModel Add(string fileName, Stream stream)
        {
            var filePath = GetFilePath(fileName);

            if (File.Exists(filePath))
                throw new ArgumentException(
                    $"The file {fileName} already exists in the file storage.");

            using (var fileStream = new FileStream(fileName, FileMode.Create))
                stream.CopyToAsync(fileStream);

            var fileInfo = new FileInfo(directoryPath);

            return new FileModel
            {
                Name = fileInfo.Name,
                Length = fileInfo.Length,
                LastModifiedDate = fileInfo.LastAccessTimeUtc
            };
        }

        public Stream Get(string fileName)
        {
            var filePath = GetFilePath(fileName);

            if (!File.Exists(filePath)) return null;

            return File.OpenRead(filePath);
        }

        public IEnumerable<FileModel> GetAll()
        {
            var directory = new DirectoryInfo(directoryPath);

            foreach (var fileInfo in directory.GetFiles())
            {
                yield return new FileModel
                {
                    Name = fileInfo.Name,
                    Length = fileInfo.Length,
                    LastModifiedDate = fileInfo.LastAccessTimeUtc
                };
            }
        }

        public FileModel Update(IFormFile file)
        {
            throw new NotImplementedException();
        }

        public FileModel Remove(string fileName)
        {
            var filePath = GetFilePath(fileName);

            if (!File.Exists(filePath)) return null;

            var fileInfo = new FileInfo(filePath);

            File.Delete(filePath);

            return new FileModel { Name = fileInfo.Name};
        }

        string GetFilePath(string fileName)
        {
            return Path.Combine(directoryPath, fileName);
        }
    }
}
