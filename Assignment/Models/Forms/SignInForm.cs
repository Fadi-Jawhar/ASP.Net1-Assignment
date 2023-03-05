using System.ComponentModel.DataAnnotations;

namespace Assignment.Models.Forms
{
    public class SignInForm
    {
        [Required]
        [EmailAddress]
        [Display(Name = "E-Mail Adress")]
        public string Email { get; set; } = null!;


        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; } = null!;
        public bool RememberMe { get; set; }

        public string? ReturnUrl { get; set; }
        
    }
}
