using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CollegeGrades.Models.AccountViewModels
{
    public class ResetPasswordViewModel
    {
        [Required]
        public string ResetKey { get; set; }
        
        [Required(ErrorMessage = "Please enter your password.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        
        [Required(ErrorMessage = "Please confirm your password.")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Your password and confirmation password do not match.")]
        [DisplayName("Confirm Password")]
        public string ConfirmPassword { get; set; }
    }
}