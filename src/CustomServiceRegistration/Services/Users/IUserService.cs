using System.Threading.Tasks;
using CustomServiceRegistration.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CustomServiceRegistration.Services.Users
{
    public interface IUserService
    {
        Task<bool> CreateAsync(RegistrationModel model);
        ModelStateDictionary ModelState { get; }
        Task<bool> EditUserAsync(UserModel model);
        Task<UserModel> GetUser(string userEmail);
    }
}
