using E_CommerceProject.Business.Brands;
using E_CommerceProject.Business.Brands.Interfaces;
using E_CommerceProject.Business.Products;
using E_CommerceProject.Business.Products.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace E_CommerceProject.Business.Shared
{
    public static class Extensions
    {
        public static IServiceCollection AddServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IBrandsService, BrandsService>();
            serviceCollection.AddTransient<IProductsService, ProductsService>();

            return serviceCollection;
        }
    }
}
