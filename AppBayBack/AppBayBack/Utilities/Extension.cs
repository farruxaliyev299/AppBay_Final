using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace AppBayBack.Utilities
{
    public static class Extension
    {
        public static bool CheckSize(this IFormFile file, int kb)
        {
            return file.Length / 1024 <= kb;
        }
        public static bool CheckType(this IFormFile file)
        {
            return file.ContentType.Contains("image/");
        }

        public async static Task<string> SaveFileAsync(this IFormFile file, string root , params string[] folders)
        {
            var fileName = Guid.NewGuid().ToString() + file.FileName;
            var resultPath = Path.Combine(GetPath(root,folders), fileName);
            using(FileStream fs = new FileStream(resultPath , FileMode.Create))
            {
                await file.CopyToAsync(fs);
            }
            return fileName;
        }

        public static string GetPath(string root , params string[] folders)
        {
            var resultPath = root;
            foreach (var folder in folders)
            {
                resultPath = Path.Combine( resultPath, folder);
            }
            return resultPath;
        }
    }
}
