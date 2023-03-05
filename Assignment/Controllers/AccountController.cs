using Assignment.Contexts;
using Assignment.Models.Entities;
using Assignment.Models.Forms;
using Assignment.Services;
using Assignment.ViewModels.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Assignment.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IdentityContext _identityContext;
        private readonly UserService _userService;
        private readonly ProfileHandler _profileHandler;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IdentityContext identityContext, UserService usersService, ProfileHandler profileHandler)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _identityContext = identityContext;
            _userService = usersService;
            _profileHandler = profileHandler;
        }

        [AllowAnonymous]
        public async Task<IActionResult> SignUp(string ReturnUrl = null!)
        {
            if (!await _userManager.Users.AnyAsync())
                return RedirectToAction("Installation", "Admin");
            else if (await _userManager.Users.CountAsync() == 1)
                return RedirectToAction("Installation", "ProductManager");
            else
            {
                var form = new SignUpForm
                {
                    ReturnUrl = ReturnUrl ?? Url.Content("~/")
                };
                return View(form);
            }

        }
        
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpForm form)
        {
            if (ModelState.IsValid)
            {
                if (await _userManager.Users.AnyAsync(x => x.Email == form.Email))
                {
                    ModelState.AddModelError(String.Empty, "A user with the same E-Mail Adress already exist.");
                    return View(form);
                }

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

                    await _userManager.AddToRoleAsync(identityUser, "User");

                    var signInResult = await _signInManager.PasswordSignInAsync(identityUser, form.Password, false, false);
                    if (signInResult.Succeeded)
                        return LocalRedirect(form.ReturnUrl);
                    else
                        return RedirectToAction("SignIn");
                }

            }

            ModelState.AddModelError(String.Empty, "Unable to create an account.");
            return View(form);
        }



        [AllowAnonymous]
        public IActionResult SignIn(string ReturnUrl = null!)
        {
            var form = new SignInForm
            {
                ReturnUrl = ReturnUrl ?? Url.Content("~/")
            };

            return View(form);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> SignIn(SignInForm form)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(form.Email, form.Password, false, false);
                if (result.Succeeded)
                    return LocalRedirect(form.ReturnUrl);
            }

            ModelState.AddModelError(string.Empty, "Incorrect email or password");
            return View(form);
        }

        public async Task<IActionResult> SignOut()
        {
            if (_signInManager.IsSignedIn(User))
                await _signInManager.SignOutAsync();
                
            return LocalRedirect("/");
        }

        public async Task<IActionResult> Edit()
        {
            var viewModel = new UserEditViewModel();
            var userAccount = await _userService.GetUserAccountAsync(User.Identity!.Name!);

            if (userAccount != null)
            {
                viewModel.UserId = userAccount.Id;
                viewModel.Form = new UserEditForm
                {
                    FirstName = userAccount.FirstName,
                    LastName = userAccount.LastName,
                    StreetName = userAccount.StreetName,
                    PostalCode = userAccount.PostalCode,
                    City = userAccount.City
                };
            }

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserEditViewModel viewModel)
        {
            var identityUser = await _identityContext.Users.FirstOrDefaultAsync(x => x.UserName == User.Identity!.Name!);
            var userProfileEntity = await _identityContext.UserProfile.FirstOrDefaultAsync(x => x.UserId == identityUser.Id);

            if (userProfileEntity != null)
            {
                userProfileEntity.FirstName = viewModel.Form.FirstName;
                userProfileEntity.LastName = viewModel.Form.LastName;
                userProfileEntity.StreetName = viewModel.Form.StreetName;
                userProfileEntity.PostalCode = viewModel.Form.PostalCode;
                userProfileEntity.City = viewModel.Form.City;

                _identityContext.Update(userProfileEntity);
                await _identityContext.SaveChangesAsync();
            }

            return RedirectToAction("Index");

        }

        public async Task<IActionResult> Index()
        {
            var viewModel = new IndexViewModel();
            viewModel.Profile = await _userService.GetUserAccountAsync(User.Identity!.Name!);
            //var userAccount = await _userService.GetUserAccountAsync(id);
            return View(viewModel);
        }
    }
}
