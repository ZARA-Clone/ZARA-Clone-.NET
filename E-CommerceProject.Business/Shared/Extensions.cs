using E_CommerceProject.Business.Brands;
using E_CommerceProject.Business.Brands.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace E_CommerceProject.Business.Shared
{
    public static class Extensions
    {
        public static IServiceCollection AddServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IBrandsService, BrandsService>();

            return serviceCollection;
        }
    }
}
