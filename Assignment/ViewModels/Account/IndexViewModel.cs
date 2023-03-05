using Assignment.Models.Identity;
using Assignment.Services;
using Microsoft.AspNetCore.Identity;

namespace Assignment.ViewModels.Account
{
    public class IndexViewModel
    {
        public UserAccount Profile { get; set; } = null!;

    }
}
