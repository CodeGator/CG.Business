using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
    public static partial class ServiceCollectionExtensions
    {
        public static IServiceCollection AddTestRepositories(
            this IServiceCollection serviceCollection,
            IConfiguration configuration,
            ServiceLifetime serviceLifetime
            )
        {
            // This method is an example of how to hook into the startup
            //   pipeline and run logic to register your specific repository
            //   types, and, of course, any types those repositories also 
            //   rely on. For instance, here is a gret place to register 
            //   you data-context type(s), if you're using EFCore.

            return serviceCollection;
        }
    }
}
