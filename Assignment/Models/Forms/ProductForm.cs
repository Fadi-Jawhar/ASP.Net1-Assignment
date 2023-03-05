using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment.Models.Forms
{
    public class ProductForm
    {
        [Required]
        [Display(Name = "ArticleNumber")]
        public string ArticleNumber { get; set; } = null!;
        
        [Required]
        [Display(Name = "Product Name")]
        public string Name { get; set; } = null!;
        
        [Required]
        [Display(Name = "Price")]
        [Column(TypeName = "money")]
        public decimal Price { get; set; }
               
        [Display(Name = "Category")]
        public string? Category { get; set; }
        
        [Required]
        [Display(Name = "Rating")]
        public int Rating { get; set; }
        
        [Display(Name = "Short Description")]
        public string? ProductDescription { get; set; }
        
        [Display(Name = "Long Description")]
        public string? ProductDescriptionLong { get; set; }
        
        public IFormFile? ProductImageName { get; set; }

        public string? ReturnUrl { get; set; }
    }
}
