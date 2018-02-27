using System.Collections.Generic;

namespace CollegeGrades.Core.Entities
{
    public class Cycle
    {
        public Cycle()
        {
            AttendedSubjects = new List<AttendedSubject>();
        }

        public string ID { get; set; }

        public int Year { get; set; }

        public int Semester { get; set; }

        public virtual ICollection<AttendedSubject> AttendedSubjects { get; set; }
    }
}