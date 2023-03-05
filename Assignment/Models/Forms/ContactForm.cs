using System.ComponentModel.DataAnnotations;

namespace Assignment.Models.Forms
{
    public class ContactForm
    {
        [Required]
        [Display(Name = "Your Name")]
        public string Name { get; set; } = null!;

        [Required]
        [EmailAddress]
        [Display(Name = "Your E-Mail Adress")]
        public string Email { get; set; } = null!;

        [Required]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; } = null!;
        public string? Company { get; set; }

        [Required]
        [Display(Name = "Write Something")]
        public string WriteSomething { get; set; } = null!;
        public bool SaveMyInfo { get; set; }


        public string? ReturnUrl { get; set; }
    }
}
