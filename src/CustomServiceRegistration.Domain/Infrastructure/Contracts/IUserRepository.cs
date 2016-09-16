using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CustomServiceRegistration.Domain.Models;

namespace CustomServiceRegistration.Domain.Infrastructure.Contracts
{
	public interface IUserRepository
	{
		Task<IEnumerable<ApplicationUser>> GetAllUsersAsync();
		Task CreateAsync(ApplicationUser user, string password);
		Task<ApplicationUser> GetUserByNameAsync(string username);
	
	}
}
