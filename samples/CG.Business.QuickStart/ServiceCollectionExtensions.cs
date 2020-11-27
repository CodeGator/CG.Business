using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
    public static partial class ServiceCollectionExtensions
    {
        public static IServiceCollection AddTestRepositories(
            this IServiceCollection serviceCollection,
            IConfiguration configuration
            )
        {
            // TODO : register concrete repository type here.
                        
            return serviceCollection;
        }
    }
}
