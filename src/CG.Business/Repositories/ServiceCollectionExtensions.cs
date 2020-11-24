using CG.Business;
using CG.Business.Properties;
using CG.Business.Repositories.Options;
using CG.Reflection;
using CG.Validations;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// This class contains extension methods related to the <see cref="IServiceCollection"/>
    /// type.
    /// </summary>
    public static partial class ServiceCollectionExtensions
    {
        // *******************************************************************
        // Public methods.
        // *******************************************************************

        #region Public methods

        /// <summary>
        /// This method registers repository types, as configured in the 
        /// specified configuration. 
        /// </summary>
        /// <param name="serviceCollection">The service collection to use 
        /// for the operation.</param>
        /// <param name="configuration">The configuration to use for the 
        /// operation.</param>
        /// <param name="assemblyBlackList">An optional black list for filtering
        /// the list of assemblies that are searched during this operation.</param>
        /// <param name="assemblyWhiteList">An optional white list for filtering
        /// the list of assemblies that are searched during this operation.</param>
        /// <returns>the value of the <paramref name="serviceCollection"/>
        /// parameter, for chaining calls together.</returns>
        /// <exception cref="ArgumentException">This exception is thrown whenever
        /// one ore more of the parameters is missing, or invalid.</exception>
        /// <exception cref="BusinessException">This exception is thrown whenver
        /// the operation can't be completed.</exception>
        public static IServiceCollection AddRepositories(
            this IServiceCollection serviceCollection,
            IConfiguration configuration,
            string assemblyWhiteList = "", 
            string assemblyBlackList = "Microsoft*, System*, mscorlib, netstandard"
            )
        {
            // Validate the parameters before attempting to use them.
            Guard.Instance().ThrowIfNull(serviceCollection, nameof(serviceCollection))
                .ThrowIfNull(configuration, nameof(configuration));

            // Get the appropriate configuration section.
            var section = configuration.GetSection(
                RepositoryOptions.SectionName
                );

            // Register the repository options with the DI container.
            serviceCollection.ConfigureOptions<RepositoryOptions>(
                section,
                out var repositoryOptions
                );

            // Trim the strategy name, just in case.
            var strategyName = repositoryOptions.Strategy.Trim();

            // Should never happen, but, pffft, check it anyway
            if (string.IsNullOrEmpty(strategyName))
            {
                // Panic!
                throw new BusinessException(
                    message: string.Format(
                        Resources.ServiceCollectionExtensions_EmptyStrategyName,
                        nameof(AddRepositories)
                        )
                    );
            }

            // Should we try to load an assembly for the repository strategy?
            if (false == string.IsNullOrEmpty(repositoryOptions.Assembly))
            {
                // Load the assembly.
                _ = Assembly.Load(repositoryOptions.Assembly);
            }

            // Format the name of a target extension method.
            var methodName = $"Add{strategyName}Repositories";
            
            // Look for specified extension method.
            var methods = AppDomain.CurrentDomain.ExtensionMethods(
                typeof(IServiceCollection),
                methodName,
                new Type[] { typeof(IConfiguration) },
                assemblyWhiteList,
                assemblyBlackList
                );

            // Did we find it?
            if (methods.Any())
            {
                // Drill down to the right configuration sub-section.
                var subSection = section.GetSection(
                    strategyName
                    );

                // We'll use the first matching method.
                var method = methods.First();

                // Invoke the extension method.
                method.Invoke(
                    null,
                    new object[] { serviceCollection, subSection }
                    );
            }
            else
            {
                // If we get here we couldn't find the extension method!

                // Panic!
                throw new BusinessException(
                    message: string.Format(
                        Resources.ServiceCollectionExtensions_MethodNotFound,
                        methodName
                        )
                    );
            }

            // Return the service collection.
            return serviceCollection;
        }

        #endregion
    }
}
