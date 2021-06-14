using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using System;

namespace CG.Business.QuickStart
{
    public static partial class ApplicationBuilderExtensions
    {
        // This methods are an example of how to hook into the startup
        //   pipeline and run logic specific to your repository types,
        //   and, of course, whatever tech those repository types rely
        //   on.  For instance, here is a great place to perform migrations,
        //   or seed an empty database.

        public static IApplicationBuilder UseTest1Repositories(
            this IApplicationBuilder applicationBuilder,
            IHostEnvironment hostEnvironment
            )
        {
            Console.WriteLine($"called {nameof(UseTest1Repositories)}");
            return applicationBuilder;
        }

        public static IApplicationBuilder UseTest2Repositories(
            this IApplicationBuilder applicationBuilder,
            IHostEnvironment hostEnvironment
            )
        {
            Console.WriteLine($"called {nameof(UseTest2Repositories)}");
            return applicationBuilder;
        }

        public static IApplicationBuilder UseTest3Repositories(
            this IApplicationBuilder applicationBuilder,
            IHostEnvironment hostEnvironment
            )
        {

            Console.WriteLine($"called {nameof(UseTest3Repositories)}");
            return applicationBuilder;
        }

        // *******************************************************************

        // This methods are an example of how to hook into the startup
        //   pipeline and run logic specific to your strategy types,
        //   and, of course, whatever tech those strategy types rely
        //   on.

        public static IApplicationBuilder UseTest1Strategies(
            this IApplicationBuilder applicationBuilder,
            IHostEnvironment hostEnvironment
            )
        {
            Console.WriteLine($"called {nameof(UseTest1Strategies)}");
            return applicationBuilder;
        }

        public static IApplicationBuilder UseTest2Strategies(
            this IApplicationBuilder applicationBuilder,
            IHostEnvironment hostEnvironment
            )
        {
            Console.WriteLine($"called {nameof(UseTest2Strategies)}");
            return applicationBuilder;
        }

        public static IApplicationBuilder UseTest3Strategies(
            this IApplicationBuilder applicationBuilder,
            IHostEnvironment hostEnvironment
            )
        {

            Console.WriteLine($"called {nameof(UseTest3Strategies)}");
            return applicationBuilder;
        }

    }
}
