using System.Collections.Generic;
using FileServer.Extensions;
using FileServer.Filters;
using FileServer.Models;
using FileServer.Services;
using FileServer.Utilites;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Net.Http.Headers;

namespace FileServer.Controllers
{
    [Route("/api/[controller]")]
    public class FilesController : Controller
    {
        readonly IFileStorage storage;

        public FilesController(IFileStorage storage)
        {
            this.storage = storage;
        }

        [HttpGet("{file}")]
        public IActionResult Get(string file)
        {
            if (string.IsNullOrEmpty(file)) return BadRequest();

            new FileExtensionContentTypeProvider()
                .TryGetContentType(file, out var contetnType);

            return File(storage.Get(file),
                contetnType ?? "application/octet-stream");
        }

        public IActionResult GetAll()
        {
            return Json(storage.GetAll());
        }

        [HttpPost]
        [DisableFormValueModelBinding]
        public IActionResult Upload()
        {
            if (!MultipartRequestHelper
                .IsMultipartContentType(Request.ContentType))
                return BadRequest();

            var defaultFormOptions = new FormOptions();

            var bondary = MultipartRequestHelper
                .GetBoundary(MediaTypeHeaderValue.Parse(Request.ContentType),
                    defaultFormOptions.MultipartBoundaryLengthLimit);

            var reader = new MultipartReader(bondary, HttpContext.Request.Body);

            var addedFiles = new List<FileModel>();

            foreach (var section in reader.ReadFileContent())
            {
                var fileName = MultipartRequestHelper
                    .ParseFileName(section.ContentDisposition);

                var addedFile = storage.Add(fileName, section.Body);

                addedFiles.Add(addedFile);
            }

            return Json(addedFiles);
        }

        [HttpDelete("{file}")]
        public IActionResult Remove(string file)
        {
            if (string.IsNullOrEmpty(file)) return BadRequest();

            var removedFile = storage.Remove(file);

            if (removedFile == null) return NotFound();

            return Json(removedFile);
        }
    }
}