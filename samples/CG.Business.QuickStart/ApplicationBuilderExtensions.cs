using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using System;

namespace CG.Business.QuickStart
{
    public static partial class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseTestRepositories(
            this IApplicationBuilder applicationBuilder,
            IWebHostEnvironment hostEnvironment,
            string configurationSection
            )
        {
            // This method is an example of how to hook into the startup
            //   pipeline and run logic specific to your repository types,
            //   and, of course, whatever tech those repository types rely
            //   on.  For instance, here is a great place to call logic
            //   to perform migrations, or seed an empty database.

            return applicationBuilder;
        }
    }
}
