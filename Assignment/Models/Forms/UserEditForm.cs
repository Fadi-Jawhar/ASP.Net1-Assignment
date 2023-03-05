using System.ComponentModel.DataAnnotations;

namespace Assignment.Models.Forms
{
    public class UserEditForm
    {
        [Required]
        [Display(Name = "Your First Name")]
        public string FirstName { get; set; } = null!;

        [Required]
        [Display(Name = "Your Last Name")]
        public string LastName { get; set; } = null!;

        [Required]
        [Display(Name = "Your Street Name")]
        public string StreetName { get; set; } = null!;

        [Required]
        [Display(Name = "Your Postal Code")]
        public string PostalCode { get; set; } = null!;

        [Required]
        [Display(Name = "Your City")]
        public string City { get; set; } = null!;
    }
}

