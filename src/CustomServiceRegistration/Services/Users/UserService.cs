using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomServiceRegistration.Domain.Infrastructure.Contracts;
using CustomServiceRegistration.Domain.Models;
using CustomServiceRegistration.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CustomServiceRegistration.Services.Users
{
	public class UserService : IUserService
	{
		private readonly IUserRepository _userRepository;
		private readonly ModelStateDictionary _modelState;
		public UserService(ModelStateDictionary modelState, IUserRepository userRepositoryRepository)
		{
			_modelState = modelState;
			_userRepository = userRepositoryRepository;
		}

		public async Task<bool> CreateAsync(RegistrationModel model)
		{
			var existingUser = await _userRepository.GetUserByNameAsync(model.UserName);

			if (existingUser != null)
			{
				_modelState.AddModelError(string.Empty, "The user already exists!");
				return false;
			}

			if (string.IsNullOrWhiteSpace(model.Password))
			{
				_modelState.AddModelError(string.Empty, "You must type a password.");
				return false;
			}

			var newUser = new ApplicationUser()
			{
				UserName = model.UserName,
				Email = model.Email,
				FirstName = model.FirstName,
				SecondName = model.SecondName,
				CountryName = model.CountryName,
				Age = model.Age,
			};
			await _userRepository.CreateAsync(newUser, model.Password);

			return true;
		}
	}
}
