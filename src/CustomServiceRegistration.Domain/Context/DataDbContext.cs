using CustomServiceRegistration.Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace CustomServiceRegistration.Domain.Context
{
	public class DataDbContext : IdentityDbContext<ApplicationUser>
	{
		public DataDbContext(DbContextOptions<DataDbContext> options) : base(options)
		{
			Database.EnsureCreated();
		}
		
	}
}
