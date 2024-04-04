using E_CommerceProject.Infrastructure.Core.Base;
using E_CommerceProject.Infrastructure.Repositories.Brands;
using E_CommerceProject.Infrastructure.Repositories.Dashborad.Data;
using E_CommerceProject.Infrastructure.Repositories.Products;
using E_CommerceProject.Infrastructure.Repositories.UserDashboardRepository;
using E_CommerceProject.Infrastructure.Repositories.UsersDashboard;
using Microsoft.Extensions.DependencyInjection;

namespace E_CommerceProject.Infrastructure.Shared
{
    public static class RepositoriesExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IBrandRepository, BrandRepository>();
            serviceCollection.AddScoped<IProductsRepository, ProductsRepository>();
            serviceCollection.AddScoped<IUserDashboardRepository, UserDashboardRepository>();
            serviceCollection.AddScoped<IDateRepository, DateRepository>();

            serviceCollection.AddScoped<IUnitOfWorkAsync, UnitOfWorkAsync>();
            return serviceCollection;
        }
    }
}
