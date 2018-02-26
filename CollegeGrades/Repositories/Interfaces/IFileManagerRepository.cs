using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace CollegeGrades.Repositories
{
    public interface IFileManagerRepository
    {
        /// <returns>Path to the file</returns>
        Task<string> Upload(IFormFile file);

        void Delete(string path);
    }
}