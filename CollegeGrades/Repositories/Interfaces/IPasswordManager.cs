namespace CollegeGrades.Repositories.Interfaces
{
    public interface IPasswordManager
    {
        string HashPassword(string password);

        bool ValidatePassword(string password, string correctHash);
    }
}