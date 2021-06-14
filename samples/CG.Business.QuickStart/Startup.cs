using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace CG.Business.QuickStart
{
    // This code demonstrates how, through the AddRepositories/UseRepositories, and
    //   AddStrategies/UseStrategies methods, along with corresponding JSON in the
    //   appSettings.json, we can dynamically load & configure repository and strategy
    //   types.

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            Console.WriteLine("REGISTERING REPOSITORIES");

            // This is how to register repositories, using the configuration
            //   to control what repository type(s) get registered, and what
            //   options are passed to those repositories, at runtime.
            services.AddRepositories(
                Configuration.GetSection("FooRep")
                );

            Console.WriteLine("REGISTERING STRATEGIES");

            // This is how to register strategies, using the configuration
            //   to control what repository type(s) get registered, and what
            //   options are passed to those strategies, at runtime.
            services.AddStrategies(
                Configuration.GetSection("FooStrat")
                );
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            Console.WriteLine("STARTING REPOSITORIES");

            // This is how to call any startup logic required for the configured
            //   repository types.
            app.UseRepositories(
                env,
                Configuration.GetSection("FooRep")
                );

            Console.WriteLine("STARTING STRATEGIES");

            // This is how to call any startup logic required for the configured
            //   strategy types.
            app.UseStrategies(
                env,
                Configuration.GetSection("FooStrat")
                );
        }
    }
}
