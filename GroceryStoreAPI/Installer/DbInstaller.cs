using GroceryStoreAPI.Repository;
using GroceryStoreAPI.Repository.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GroceryStoreAPI.Installer
{
    public class DbInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IRepositoryProvider, JsonRepository>();
        }
    }
}
