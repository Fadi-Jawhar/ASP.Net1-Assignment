using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Eventing.Reader;

namespace Assignment.Models.Forms
{
    public class SignUpForm
    {
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; } = null!;

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; } = null!;

        [Required]
        [EmailAddress]
        [Display(Name = "E-Mail Adress")]
        public string Email { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; } = null!;

        [Required]
        [Display(Name = "Street Name")]
        public string StreetName { get; set; } = null!;
        [Required]
        [Display(Name = "Postal Code")]
        public string PostalCode { get; set; } = null!;
        [Required]
        [Display(Name = "City")]
        public string City { get; set; } = null!;
        [Display(Name = "Phone Number")]
        public string? PhoneNumber { get; set; }
        public string? Company { get; set; }
        public IFormFile? ProfileImage { get; set; }

        public bool TermsAndAgreements { get; set; }

        
        public string? ReturnUrl { get; set; } 
    }

}
