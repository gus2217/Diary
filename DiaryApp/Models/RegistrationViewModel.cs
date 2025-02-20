using System.ComponentModel.DataAnnotations;

namespace DiaryApp.Models
{
    public class RegistrationViewModel
    {
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last Name is required.")]
        [MaxLength(50, ErrorMessage = ("Max 50 characters allowed."))]
        public string LastName { get; set; }
        [Required(ErrorMessage = ("Email is required."))]
        [MaxLength(100, ErrorMessage = ("Max 100 characters allowed."))]
        [EmailAddress(ErrorMessage =("Please enter a valid email."))]

        public string Email { get; set; }
        [Required(ErrorMessage = ("Username is required."))]
        [MaxLength(20, ErrorMessage = ("Max 20 characters allowed."))]
        public string UserName { get; set; }
        [Required(ErrorMessage = ("Password is required."))]
        [StringLength(20, MinimumLength =5, ErrorMessage = ("Max 20 or min 5 characters allowed."))]
        [DataType(DataType.Password)]

        public string Password { get; set; }
        [Compare("Password", ErrorMessage =("Please confirm password."))]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
