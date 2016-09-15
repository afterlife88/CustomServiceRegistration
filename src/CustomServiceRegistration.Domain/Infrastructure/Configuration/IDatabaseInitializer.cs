using System.Threading.Tasks;

namespace CustomServiceRegistration.Domain.Infrastructure.Configuration
{
    public interface IDatabaseInitializer
    {
		Task Seed();
	}
}
