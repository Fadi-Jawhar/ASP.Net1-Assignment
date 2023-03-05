using Assignment.Models.Entities;

namespace Assignment.ViewModels.Products
{
    public class ProductsViewModel
    {
        public ICollection<ProductEntity> Products { get; set; } = new List<ProductEntity>();

    }
}
