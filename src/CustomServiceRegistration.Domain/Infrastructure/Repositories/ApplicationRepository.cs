using System;
using System.Linq;
using System.Threading.Tasks;
using CustomServiceRegistration.Domain.Context;
using CustomServiceRegistration.Domain.Infrastructure.Contracts;
using CustomServiceRegistration.Domain.Models;

namespace CustomServiceRegistration.Domain.Infrastructure.Repositories
{
    public class ApplicationRepository : IApplicationRepository
    {
        private readonly DataDbContext _dataDbContext;

        public ApplicationRepository(DataDbContext dataDbContext)
        {
            _dataDbContext = dataDbContext;
        }
        public async Task<int> Create(Application model)
        {
            _dataDbContext.Applications.Add(model);
            return await _dataDbContext.SaveChangesAsync();
        }

        public bool CheckIfAlreadyExist(Application model)
        {
            var checker =  _dataDbContext.Applications.FirstOrDefault(r => r.ApplicationName == model.ApplicationName);
            if (checker == null)
                return false;
            return true;
        }
    }
}
