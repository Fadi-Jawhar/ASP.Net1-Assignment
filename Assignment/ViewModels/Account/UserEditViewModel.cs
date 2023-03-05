using Assignment.Models.Forms;
using Microsoft.AspNetCore.Identity;

namespace Assignment.ViewModels.Account
{
    public class UserEditViewModel : IdentityUser
    {
        public string UserId { get; set; } = null!;
        public UserEditForm Form { get; set; } = new UserEditForm();

    }
}
