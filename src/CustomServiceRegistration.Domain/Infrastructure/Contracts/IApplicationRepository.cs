using System.Threading.Tasks;
using CustomServiceRegistration.Domain.Models;

namespace CustomServiceRegistration.Domain.Infrastructure.Contracts
{
    public interface IApplicationRepository
    {
        Task<int> Create(Application model);
    }
}
