using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace CG.Business.QuickStart
{
    class Program
    {
        static void Main(string[] args)
        {
            // Build a typical configuration.
            var builder = new ConfigurationBuilder();
            builder.AddJsonFile("appSettings.json");
            var configuration = builder.Build();

            // Build an empty DI container.
            var services = new ServiceCollection();

            // This is how to add repositories, using the configuration
            //   to control what repository types get registered, and what
            //   options are passed to those repositories, at runtime.
            services.AddRepositories(configuration);

        }            
    }
}
