using System.ComponentModel.DataAnnotations.Schema;

namespace CollegeGrades.Models
{
    public class Score
    {
        public string ID { get; set; }

        public string AttendedSubjectID { get; set; }

        public string Name { get; set; }

        public float Value { get; set; }

        [ForeignKey(nameof(AttendedSubjectID))]
        public virtual AttendedSubject AttendedSubject { get; set; }
    }
}