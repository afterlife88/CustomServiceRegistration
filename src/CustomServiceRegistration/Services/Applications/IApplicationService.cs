using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomServiceRegistration.Services.Applications
{
    public interface IApplicationService
    {
        Task<bool> AddApplicationAsync(string appName);
    }
}
