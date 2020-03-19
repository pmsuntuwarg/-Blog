using System.ComponentModel.DataAnnotations;

namespace Blog.Entities.ViewModels.Account
{
    public class ResetPasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Password donot match.")]
        public string ConfirmPassword { get; set; }
    }
}
