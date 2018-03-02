using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace CollegeGrades.Models.AccountViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Please enter your email.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        [Remote("VerifyEmail", "Account")]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "Please enter your password.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        
        [Required(ErrorMessage = "Please confirm your password.")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Your password and confirmation password do not match.")]
        [DisplayName("Confirm Password")]
        public string ConfirmPassword { get; set; }
        
        [Required(ErrorMessage = "Please enter your first name.")]
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        
        [Required(ErrorMessage = "Please enter your last name.")]
        [DisplayName("Last Name")]
        public string LastName { get; set; }
    }
}