using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomServiceRegistration.Domain.Infrastructure.Contracts;
using CustomServiceRegistration.Domain.Models;

namespace CustomServiceRegistration.Services.Applications
{
    public class ApplicationService : IApplicationService
    {
        private readonly IApplicationRepository _applicationRepository;
        public ApplicationService(IApplicationRepository applicationRepository)
        {
            _applicationRepository = applicationRepository;
        }
        public async Task<bool> AddApplicationAsync(string appName)
        {
            var exist = _applicationRepository.CheckIfAlreadyExist(new Application()
            {
                ApplicationName = appName
            });
            if (exist)
                return false;
            await _applicationRepository.Create(new Application() { ApplicationName = appName });
            return true;
        }
    }
}
