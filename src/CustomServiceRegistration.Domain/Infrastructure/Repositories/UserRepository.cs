using System.Collections.Generic;
using System.Threading.Tasks;
using CustomServiceRegistration.Domain.Infrastructure.Contracts;
using CustomServiceRegistration.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CustomServiceRegistration.Domain.Infrastructure.Repositories
{
	public class UserRepository : IUserRepository
	{
		private readonly UserManager<ApplicationUser> _userManager;


		public UserRepository(UserManager<ApplicationUser> userManager)
		{
			_userManager = userManager;
		}

		public async Task<IEnumerable<ApplicationUser>> GetAllUsersAsync()
		{
			return await _userManager.Users.ToArrayAsync();
		}

		public async Task CreateAsync(ApplicationUser user, string password)
		{
			await _userManager.CreateAsync(user, password);
		}

		public async Task EditAsync(ApplicationUser user)
		{
			await _userManager.UpdateAsync(user);
		}
		public async Task<ApplicationUser> GetUserByNameAsync(string username)
		{
			return await _userManager.FindByNameAsync(username);
		}

		private bool _disposed;
		public void Dispose()
		{
			if (!_disposed)
			{
				_userManager.Dispose();
			
			}
			_disposed = true;
		}
	}
}
