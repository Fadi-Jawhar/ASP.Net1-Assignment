namespace Assignment.Services
{
    public class ProfileHandler
    {
        private readonly IWebHostEnvironment _env;

        public ProfileHandler(IWebHostEnvironment webHostEnvironment)
        {
            _env = webHostEnvironment;
        }

        public async Task<string> UploadProfileImageAsync(IFormFile profileImage)
        {
            var profilePath = $"{_env.WebRootPath}/images/profiles";
            var imageName = $"profile_{Guid.NewGuid()}{Path.GetExtension(profileImage.FileName)}";
            string filePath = $"{profilePath}/{imageName}";

            using var fs = new FileStream(filePath, FileMode.Create);
            await profileImage.CopyToAsync(fs);

            return imageName;
        }
    }
}
