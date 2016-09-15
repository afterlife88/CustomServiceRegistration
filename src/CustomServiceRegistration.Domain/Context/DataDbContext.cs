using CustomServiceRegistration.Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CustomServiceRegistration.Domain.Context
{
	public class DataDbContext : IdentityDbContext<ApplicationUser>
	{
		public DataDbContext(DbContextOptions<DataDbContext> options) : base(options)
		{

		}
		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);
		}
	}
}
