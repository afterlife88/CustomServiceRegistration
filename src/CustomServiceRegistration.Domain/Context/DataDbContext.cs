using CustomServiceRegistration.Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CustomServiceRegistration.Domain.Context
{
    public class DataDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Application> Applications { get; set; }
        public DataDbContext(DbContextOptions<DataDbContext> options) : base(options)
        {
           
        }

    }
}
