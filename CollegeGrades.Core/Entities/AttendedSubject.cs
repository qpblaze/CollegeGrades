using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CollegeGrades.Core.Entities
{
    public class AttendedSubject
    {
        public AttendedSubject()
        {
            Scores = new List<Score>();
        }

        public string ID { get; set; }

        public string AccountID { get; set; }

        public string SubjectID { get; set; }

        public string TeacherID { get; set; }

        public string CycleID { get; set; }

        [ForeignKey(nameof(AccountID))]
        public virtual Account Account { get; set; }

        [ForeignKey(nameof(SubjectID))]
        public virtual Subject Subject { get; set; }

        [ForeignKey(nameof(TeacherID))]
        public virtual Teacher Teacher { get; set; }

        [ForeignKey(nameof(CycleID))]
        public virtual Cycle Cycle { get; set; }

        public virtual ICollection<Score> Scores { get; set; }
    }
}