using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace CustomServiceRegistration.Domain.Models
{
	public class ApplicationUser : IdentityUser
	{
		public string FirstName { get; set; }
		public string SecondName { get; set; }
		public string CountryName { get; set; }
		public int Age { get; set; }
	}
}
