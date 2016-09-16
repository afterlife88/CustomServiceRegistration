using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomServiceRegistration.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CustomServiceRegistration.Services.Users
{
    public interface IUserService
    {
        Task<bool> CreateAsync(RegistrationModel model);
        ModelStateDictionary ModelState { get; }
    }
}
