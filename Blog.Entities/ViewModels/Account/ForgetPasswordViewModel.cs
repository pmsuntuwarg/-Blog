using System.ComponentModel.DataAnnotations;

namespace Blog.Entities.ViewModels.Account
{
    public class ForgetPasswordViewModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
