using Microsoft.Extensions.Configuration;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static partial class ServiceCollectionExtensions
    {
        // These methods are an example of how to hook into the registration
        //   pipeline and run logic to register your specific repository
        //   types, and, of course, any types those repositories also 
        //   rely on. For instance, here is a great place to register 
        //   you data-context type(s), options, etc.

        public static IServiceCollection AddTest1Repositories(
            this IServiceCollection serviceCollection,
            IConfiguration configuration,
            ServiceLifetime serviceLifetime
            )
        {
            // 1st variant, accepts a trailing ServiceLifetime parameter, passed
            //   from the UseRepositories/UpStrategies methods.

            Console.WriteLine($"called {nameof(AddTest1Repositories)}");
            return serviceCollection;
        }

        public static IServiceCollection AddTest2Repositories(
            this IServiceCollection serviceCollection,
            IConfiguration configuration
            )
        {
            // 2nd variant, without the trailing ServiceLifetime parameter.

            Console.WriteLine($"called {nameof(AddTest2Repositories)}");
            return serviceCollection;
        }

        public static IServiceCollection AddTest3Repositories(
            this IServiceCollection serviceCollection,
            IConfiguration configuration
            )
        {
            // 2nd variant, without the trailing ServiceLifetime parameter.

            Console.WriteLine($"called {nameof(AddTest3Repositories)}");
            return serviceCollection;
        }

        // *******************************************************************

        // These methods are an example of how to hook into the registration
        //   pipeline and run logic to register your specific strategy types,
        //   and, of course, any types those strategiess also rely on. 

        public static IServiceCollection AddTest1Strategies(
            this IServiceCollection serviceCollection,
            IConfiguration configuration,
            ServiceLifetime serviceLifetime
            )
        {
            // 1st variant, accepts a trailing ServiceLifetime parameter, passed
            //   from the UseRepositories/UpStrategies methods.

            Console.WriteLine($"called {nameof(AddTest1Strategies)}");
            return serviceCollection;
        }

        public static IServiceCollection AddTest2Strategies(
            this IServiceCollection serviceCollection,
            IConfiguration configuration
            )
        {
            // 2nd variant, without the trailing ServiceLifetime parameter.

            Console.WriteLine($"called {nameof(AddTest2Strategies)}");
            return serviceCollection;
        }

        public static IServiceCollection AddTest3Strategies(
            this IServiceCollection serviceCollection,
            IConfiguration configuration
            )
        {
            // 2nd variant, without the trailing ServiceLifetime parameter.

            Console.WriteLine($"called {nameof(AddTest3Strategies)}");
            return serviceCollection;
        }
    }
}
