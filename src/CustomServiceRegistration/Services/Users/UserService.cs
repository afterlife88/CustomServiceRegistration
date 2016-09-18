using System;
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
        public ModelStateDictionary ModelState { get; }
        public UserService(ModelStateDictionary modelState, IUserRepository userRepositoryRepository)
        {
            ModelState = modelState;
            _userRepository = userRepositoryRepository;
        }

        public async Task<bool> CreateAsync(RegistrationModel model)
        {
            if (model.UserName.Split(' ').Length == 2)
            {
                ModelState.AddModelError(string.Empty, "Username should not contain spaces!");
                return false;
            }
            var existingUser = await _userRepository.GetUserByNameAsync(model.UserName);

            if (existingUser != null)
            {
                ModelState.AddModelError(string.Empty, "The user already exists!");
                return false;
            }

            if (string.IsNullOrWhiteSpace(model.Password))
            {
                ModelState.AddModelError(string.Empty, "You must type a password.");
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
        public async Task<bool> EditUserAsync(UserModel model)
        {
            var existedUser = await _userRepository.GetUserByNameAsync(model.UserName);
            if (existedUser == null)
            {
                ModelState.AddModelError(string.Empty, "The user not exist...");
                return false;
            }
            // Updating data
            existedUser.FirstName = model.FirstName;
            existedUser.SecondName = model.SecondName;
            existedUser.Email = model.Email;
            existedUser.UserName = model.UserName;
            existedUser.Age = model.Age;
            existedUser.CountryName = model.CountryName;

            await _userRepository.EditAsync(existedUser);
            return true;
        }

        public async Task<UserModel> GetUser(string userEmail)
        {
            var user = await _userRepository.GetUser(userEmail);

            if (user == null)
            {
                return null;
            }

            var userModel = new UserModel()
            {
                SecondName = user.SecondName,
                CountryName = user.CountryName,
                Age = user.Age,
                FirstName = user.FirstName,
                UserName = user.UserName,
                Email = user.Email
            };
            return userModel;
        }

        public async Task<UserModel> GetUserById(string userId)
        {
            var user = await _userRepository.GetUserByID(userId);

            if (user == null)
            {
                return null;
            }

            var userModel = new UserModel()
            {
                SecondName = user.SecondName,
                CountryName = user.CountryName,
                Age = user.Age,
                FirstName = user.FirstName,
                UserName = user.UserName,
                Email = user.Email
            };
            return userModel;
        }
    }

}
