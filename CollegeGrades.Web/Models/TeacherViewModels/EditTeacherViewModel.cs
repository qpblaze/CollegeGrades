using System.ComponentModel.DataAnnotations;

namespace CollegeGrades.Web.Models.TeacherViewModels
{
    public class EditTeacherViewModel
    {
        [Required]
        public string ID { get; set; }

        [Required(ErrorMessage = "Please enter his name.")]
        public string Name { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Please enter his email.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter the link to his website.")]
        public string Link { get; set; }
    }
}