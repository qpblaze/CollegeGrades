using CollegeGrades.Core.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CollegeGrades.Core.Entities
{
    public class Account
    {
        public Account()
        {
            AttendedSubjects = new List<AttendedSubject>();
        }

        public string ID { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Password { get; set; }

        public string PhoneNumber { get; set; }

        public bool IsVerified { get; set; }

        public string ConfirmEmailToken { get; set; }

        public string ResetPasswordToken { get; set; }

        public string ProfileImage { get; set; }

        /// <summary>
        /// User's role id which is store in database
        /// </summary>
        public virtual int RoleTypeID
        {
            get => (int)RoleType;
            set => RoleType = (RoleType)value;
        }

        /// <summary>
        /// User role that can be accessed anywhere
        /// </summary>
        [NotMapped]
        public RoleType RoleType { get; set; }

        public virtual ICollection<AttendedSubject> AttendedSubjects { get; set; }
    }
}