namespace CollegeGrades.Core.Interfaces
{
    public interface IFileManager
    {
        /// <returns>Path to the file</returns>
        // Task<string> Upload(IFormFile file);

        void Delete(string path);
    }
}