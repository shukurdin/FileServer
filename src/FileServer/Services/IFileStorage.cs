using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using FileServer.Models;
using Microsoft.AspNetCore.Http;

namespace FileServer.Services
{
    /// <summary>
    ///     Provides simple CRUD operations.
    /// </summary>
    public interface IFileStorage
    {
        FileModel Add(string fileName, Stream stream);
        Stream Get(string fileName);
        IEnumerable<FileModel> GetAll();
        FileModel Update(IFormFile file);
        FileModel Remove(string fileName);
    }
}