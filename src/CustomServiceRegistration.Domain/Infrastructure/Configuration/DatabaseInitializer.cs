using System.Linq;
using System.Threading.Tasks;
using CustomServiceRegistration.Domain.Context;
using CustomServiceRegistration.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace CustomServiceRegistration.Domain.Infrastructure.Configuration
{
    public class DatabaseInitializer : IDatabaseInitializer
    {
        private readonly DataDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DatabaseInitializer(DataDbContext context, UserManager<ApplicationUser> userManager,
             RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _context = context;
            _roleManager = roleManager;
        }

        public async Task Seed()
        {

            await _context.Database.EnsureCreatedAsync();


            if (_context.Users.Any())
            {
                foreach (var u in _context.Users)
                    _context.Remove(u);
                _context.SaveChanges();
            }
            if (_context.Applications.Any())
            {
                foreach (var item in _context.Applications)
                    _context.Remove(item);
                _context.SaveChanges();
            }

            _context.Applications.Add(new Application()
            {
                ApplicationName = "test"
            });

            var email = "test@devchallenge.it";
            if (await _userManager.FindByEmailAsync(email) == null)
            {
                // use the create rather than addorupdate so can set password
                var user = new ApplicationUser
                {
                    UserName = "admin",
                    Email = email,
                    Age = 21,
                    FirstName = "John",
                    SecondName = "Snow",
                    CountryName = "Vesteros",


                };
                await _userManager.CreateAsync(user, "test123456");
            }
      

            var createdUser = await _userManager.FindByEmailAsync(email);
            var roleName = "admin";
            if (await _roleManager.FindByNameAsync(roleName) == null)
            {
                await _roleManager.CreateAsync(new IdentityRole() { Name = roleName });
            }

            if (!await _userManager.IsInRoleAsync(createdUser, roleName))
            {
                await _userManager.AddToRoleAsync(createdUser, roleName);
            }
            await _context.SaveChangesAsync();

        }
    }
}
