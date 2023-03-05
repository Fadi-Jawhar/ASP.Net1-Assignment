using Assignment.Models.Identity;

namespace Assignment.ViewModels.Admin
{
    public class AdminEditUserViewModel
    {
        public ICollection<UserAccount> Users { get; set; } = new List<UserAccount>();
    }
}
