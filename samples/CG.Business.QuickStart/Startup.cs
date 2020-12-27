using CG.Business.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace CG.Business.QuickStart
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // This is how to register repositories, using the configuration
            //   to control what repository types get registered, and what
            //   options are passed to those repositories, at runtime.
            services.AddRepositories(Configuration);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // This is how to call any startup logic required for the configured
            //   repository types - for instance, EFCore types might need to apply
            //   migrations.
            app.UseRepositories(env);
        }
    }
}
