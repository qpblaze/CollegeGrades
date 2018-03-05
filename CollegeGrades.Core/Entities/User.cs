using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace CollegeGrades.Core.Entities
{
    public class User : IdentityUser
    {
        public User()
        {
            AttendedSubjects = new List<AttendedSubject>();
        }

        public virtual ICollection<AttendedSubject> AttendedSubjects { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ProfileImage { get; set; }
        public string CoverImage { get; set; }
    }
}