using CollegeGrades.Core.Interfaces;
using System;

namespace CollegeGrades.Infrastructure.Services
{
    public class FileManager : IFileManager
    {
        //public IHostingEnvironment _hostingEnvironment;

        //public FileManagerService(IHostingEnvironment hostingEnvironment)
        //{
        //    _hostingEnvironment = hostingEnvironment;
        //}

        //public async Task<string> Upload(IFormFile file)
        //{
        //    var aux = file.FileName.Split('.');
        //    string extension = aux[aux.Length - 1];

        //    string newFileName = Guid.NewGuid().ToString() + "." + extension;
        //    string filePath = Path.Combine("uploads", newFileName);
        //    string path = Path.Combine(_hostingEnvironment.WebRootPath, filePath);

        //    if (file.Length > 0)
        //    {
        //        using (var stream = new FileStream(path, FileMode.Create))
        //        {
        //            await file.CopyToAsync(stream);
        //        }
        //    }

        //    return @"\" + filePath;
        //}

        //public void Delete(string path)
        //{
        //    path = _hostingEnvironment.WebRootPath + path;
        //    FileInfo file = new FileInfo(path);

        //    if (file.Exists)
        //    {
        //        file.Delete();
        //    }
        //}
        public void Delete(string path)
        {
            throw new NotImplementedException();
        }
    }
}