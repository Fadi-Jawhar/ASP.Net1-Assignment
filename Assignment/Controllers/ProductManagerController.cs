using Assignment.Contexts;
using Assignment.Models.Entities;
using Assignment.Models.Forms;
using Assignment.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Assignment.Controllers
{
    [Authorize(Roles = "Administrator, ProductManager")]
    public class ProductManagerController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IdentityContext _identityContext;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserService _userService;
        private readonly ProfileHandler _profileHandler;
        private readonly DataContext _dataContext;
        private readonly ProductService _productService;

        public ProductManagerController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IdentityContext identityContext, RoleManager<IdentityRole> roleManager, UserService userService, ProfileHandler profileHandler, DataContext dataContext, ProductService productService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _identityContext = identityContext;
            _roleManager = roleManager;
            _userService = userService;
            _profileHandler = profileHandler;
            _dataContext = dataContext;
            _productService = productService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Installation(string ReturnUrl = null!)
        {

            if (await _userManager.Users.CountAsync() == 2)
                return RedirectToAction("SingIn", "Account");

            var form = new SignUpForm
            {
                ReturnUrl = ReturnUrl ?? Url.Content("~/")
            };

            return View(form);


        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Installation(SignUpForm form)
        {
            if (ModelState.IsValid)
            {              
                var identityUser = new IdentityUser
                {
                    Email = form.Email,
                    UserName = form.Email
                };

                var result = await _userManager.CreateAsync(identityUser, form.Password);
                if (result.Succeeded)
                {
                    _identityContext.UserProfile.Add(new UserProfileEntity
                    {
                        UserId = identityUser.Id,
                        FirstName = form.FirstName,
                        LastName = form.LastName,
                        StreetName = form.StreetName,
                        PostalCode = form.PostalCode,
                        City = form.City,
                        PhoneNumber = form.PhoneNumber ?? null,
                        Company = form.Company ?? null!,
                        ImageName = await _profileHandler.UploadProfileImageAsync(form.ProfileImage) ?? null!
                    });

                    await _identityContext.SaveChangesAsync();

                    await _userManager.AddToRoleAsync(identityUser, "ProductManager");


                    var signInResult = await _signInManager.PasswordSignInAsync(identityUser, form.Password, false, false);
                    if (signInResult.Succeeded)
                        return LocalRedirect(form.ReturnUrl);
                    else
                        return RedirectToAction("SignIn", "Account");
                }

            }

            ModelState.AddModelError(String.Empty, "Unable to create an account.");
            return View(form);
        }

        public async Task<IActionResult> AddProduct(string ReturnUrl = null!)
        {
            var form = new ProductForm
            {
                ReturnUrl = ReturnUrl ?? Url.Content("~/")
            };
            return View(form);
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(ProductForm form)
        {
            if (ModelState.IsValid)
            {

                _dataContext.Products.Add(new ProductEntity
                {
                    ArticleNumber = form.ArticleNumber,
                    Name = form.Name,
                    Price = form.Price,
                    Category = form.Category,
                    Rating = form.Rating,
                    ProductDescription = form.ProductDescription,
                    ProductDescriptionLong = form.ProductDescriptionLong ?? null!,
                    ProductImageName = await _productService.UploadProductImageNameAsync(form.ProductImageName) ?? null!
                });
                await _dataContext.SaveChangesAsync();
                return RedirectToAction("Index", "Account");
            }

            ModelState.AddModelError(string.Empty, "Unable to add a Product.");
            return View(form);
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}






