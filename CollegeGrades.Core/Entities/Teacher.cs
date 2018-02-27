using System.Collections.Generic;

namespace CollegeGrades.Core.Entities
{
    public class Teacher
    {
        public Teacher()
        {
            AttendedSubjects = new List<AttendedSubject>();
        }

        public string ID { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Link { get; set; }

        public virtual ICollection<AttendedSubject> AttendedSubjects { get; set; }
    }
}