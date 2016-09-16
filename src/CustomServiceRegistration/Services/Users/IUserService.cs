using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomServiceRegistration.Models;

namespace CustomServiceRegistration.Services.Users
{
	public interface IUserService
	{
		Task<bool> CreateAsync(RegistrationModel model);
	}
}
