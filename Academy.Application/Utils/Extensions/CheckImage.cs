using System.IO;
using Microsoft.AspNetCore.Http;

namespace Academy.Application.Extensions
{
    public static class FileChecker
    {

        public static bool IsCompressFile(this IFormFile file)
        {
            string ex = Path.GetExtension(file.FileName);

            return ex.Contains("rar") || ex.Contains("zip");
        }
    }
}
