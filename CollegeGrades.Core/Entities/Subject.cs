using System.Collections.Generic;

namespace CollegeGrades.Core.Entities
{
    public class Subject
    {
        public Subject()
        {
            AttendedSubjects = new List<AttendedSubject>();
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<AttendedSubject> AttendedSubjects { get; set; }
    }
}