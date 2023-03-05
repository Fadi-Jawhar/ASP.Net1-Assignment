namespace Assignment.Services
{
    public class ProductService
    {
        private readonly IWebHostEnvironment _env;

        public ProductService(IWebHostEnvironment webHostEnvironment)
        {
            _env = webHostEnvironment;
        }

        public async Task<string> UploadProductImageNameAsync(IFormFile productimagename)
        {
            var productPath = $"{_env.WebRootPath}/images/products";
            var productImage = $"product_{Guid.NewGuid()}{Path.GetExtension(productimagename.FileName)}";
            string filePath = $"{productPath}/{productImage}";

            using var fs = new FileStream(filePath, FileMode.Create);
            await productimagename.CopyToAsync(fs);

            return productImage;
        }
    }
}
