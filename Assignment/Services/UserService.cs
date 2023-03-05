using Assignment.Contexts;
using Assignment.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Assignment.Services
{
    public class UserService
    {
        private readonly IdentityContext _context;

        public UserService(IdentityContext context)
        {          
            _context = context;
        }

        public async Task<UserAccount> GetUserAccountAsync(string userName)
        {
            var identityUser = await _context.Users.FirstOrDefaultAsync(x => x.UserName == userName);
            if (identityUser != null)
            {
                var userProfileEntity = await _context.UserProfile.FirstOrDefaultAsync(x => x.UserId == identityUser.Id);

                var identityProfile = await _context.UserProfile.FirstOrDefaultAsync(x => x.UserId == identityUser.Id);
                if (userProfileEntity != null)
                {
                    return new UserAccount
                    {
                        Id = identityUser.Id,
                        FirstName = userProfileEntity.FirstName,
                        LastName = userProfileEntity.LastName,
                        Email = identityUser.Email!,
                        StreetName = userProfileEntity.StreetName,
                        PostalCode = userProfileEntity.PostalCode,
                        City = userProfileEntity.City,
                        PhoneNumber = userProfileEntity.PhoneNumber,
                        Company = userProfileEntity.Company,
                        ImageName = userProfileEntity.ImageName
                    };
                }
            }
            return null!;
        }
    }
}
