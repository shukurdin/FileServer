using System;
using System.IO;
using System.Linq;
using Microsoft.Net.Http.Headers;

namespace FileServer.Utilites
{
    // This method is similar to the example from Microsoft docs:
    // github.com/aspnet/Docs/blob/master/aspnetcore/
    // mvc/models/file-uploads/sample/FileUploadSample/MultipartRequestHelper.cs
    public static class MultipartRequestHelper
    {
        public static string GetBoundary(
            MediaTypeHeaderValue contentType,
            int lengthLimit)
        {
            var boundary = HeaderUtilities
                .RemoveQuotes(contentType.Boundary);

            if (string.IsNullOrWhiteSpace(boundary.Value))
                throw new InvalidDataException(
                    "Missing contetn-type bondary.");

            if (boundary.Length > lengthLimit)
                throw new InvalidDataException(
                    $"Multipart boundary length limit {lengthLimit} exceeded.");

            return boundary.Value;
        }

        public static bool IsMultipartContentType(string contentType)
        {
            return !string.IsNullOrEmpty(contentType)
                   && contentType.IndexOf("multipart/",
                       StringComparison.OrdinalIgnoreCase) >= 0;
        }

        public static bool HasFormDataContentDisposition(
            ContentDispositionHeaderValue disposition)
        {
            // Content-Disposition: form-data; name="key";
            return disposition != null
                   && disposition.DispositionType.Equals("form-data")
                   && string.IsNullOrEmpty(disposition.FileName.Value)
                   && string.IsNullOrEmpty(disposition.FileNameStar.Value);
        }

        public static bool HasFileContentDisposition(
            ContentDispositionHeaderValue contentDisposition)
        {
            // Content - Disposition: form - data; name = "myfile1"; filename = "Misc 002.jpg"
            return contentDisposition != null
                   && contentDisposition.DispositionType.Equals("form-data")
                   && (!string.IsNullOrEmpty(contentDisposition.FileName.Value)
                       || !string.IsNullOrEmpty(contentDisposition.FileNameStar.Value));
        }

        public static string ParseFileName(string contentDisposition)
        {
            var fullPath = contentDisposition.Split(';')
                .SingleOrDefault(part => part.Contains("filename"))
                ?.Split('=')
                .Last()
                .Trim('"');

            return Path.GetFileName(fullPath);
        }
    }
}