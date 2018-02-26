using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace CollegeGrades.Repositories
{
    public class FileManagerRepository : IFileManagerRepository
    {
        public IHostingEnvironment _hostingEnvironment;

        public FileManagerRepository(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }


        public async Task<string> Upload(IFormFile file)
        {
            var aux = file.FileName.Split('.');
            string extension = aux[aux.Length - 1];

            string newFileName = Guid.NewGuid().ToString() + "." + extension;
            string filePath = Path.Combine("uploads", newFileName);
            string path = Path.Combine(_hostingEnvironment.WebRootPath, filePath);

            if (file.Length > 0)
            {
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }

            return @"\" + filePath;
        }
        
        public void Delete(string path)
        {
            path = _hostingEnvironment.WebRootPath + path;
            FileInfo file = new FileInfo(path);

            if(file.Exists)
            {
                file.Delete();
            }
        }
    }
}