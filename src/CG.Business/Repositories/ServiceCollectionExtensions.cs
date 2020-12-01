using CG.Business;
using CG.Business.Builders;
using CG.Business.Options;
using CG.Business.Properties;
using CG.Business.Repositories.Options;
using CG.DataAnnotations;
using CG.Options;
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
        /// This method dynamically registers repository types, as configured 
        /// in the specified configuration section. 
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
        /// <remarks>
        /// The idea, with this method, is to allow the caller to specify the
        /// concrete repository type(s) in the configuration. If configured to 
        /// do so, this method will load an assembly in order to resolve the 
        /// extension method(s) needed to register the repository types.  
        /// </remarks>
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
                "Repositories"
                );

            // Create the loader options.
            var loaderOptions = new LoaderOptions();

            // Bind the loader options to the configuration.
            section.Bind(loaderOptions);

            // Verify the options.
            (loaderOptions as OptionsBase)?.ThrowIfInvalid();

            // Trim the strategy name, just in case.
            var strategyName = loaderOptions.Strategy.Trim();

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

            try
            {
                // Should we try to load an assembly for the repository strategy?
                if (false == string.IsNullOrEmpty(loaderOptions.Assembly))
                {
                    // Load the assembly.
                    _ = Assembly.Load(loaderOptions.Assembly);

                    // If an assembly was specified then we should be able to
                    // make use of the white list to significantly improve 
                    //   the runtime of the search operation we're about to
                    //   perform.
                    assemblyWhiteList = assemblyWhiteList.Length > 0
                        ? $"{assemblyWhiteList}, {loaderOptions.Assembly}"
                        : assemblyWhiteList;
                }
            }
            catch (Exception ex)
            {
                // Provide better context for the error.
                throw new BusinessException(
                    message: string.Format(
                        Resources.ServiceCollectionExtensions_NoLoadAssembly,
                        loaderOptions.Assembly
                        ),
                    innerException: ex
                    );
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
                // Drill down into the appropriate sub-section.
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
                // If we get here we found the assembly but couldn't find a matching
                //   extension method!

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

        // *******************************************************************

        /// <summary>
        /// This method dynamically registers repository types, as configured 
        /// in the specified configuration section. This variant is intended to 
        /// work with custom builders derived from the <see cref="BuilderBase"/>
        /// class.
        /// </summary>
        /// <typeparam name="TBuilder">The type of builder to use for internal
        /// extension method search.</typeparam>
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
        /// <remarks>
        /// The idea, with this method, is to allow the caller to specify the
        /// concrete repository type(s) in the configuration. If configured to 
        /// do so, this method will load an assembly in order to resolve the 
        /// extension method(s) needed to register the repository types.  
        /// </remarks>
        public static IServiceCollection AddRepositories<TBuilder>(
            this IServiceCollection serviceCollection,
            IConfiguration configuration,
            string assemblyWhiteList = "",
            string assemblyBlackList = "Microsoft*, System*, mscorlib, netstandard"
            ) where TBuilder : BuilderBase, new()
        {
            // Validate the parameters before attempting to use them.
            Guard.Instance().ThrowIfNull(serviceCollection, nameof(serviceCollection))
                .ThrowIfNull(configuration, nameof(configuration));

            // Get the appropriate configuration section.
            var section = configuration.GetSection(
                "Repositories"
                );

            // Create the loader options.
            var loaderOptions = new LoaderOptions();

            // Bind the loader options to the configuration.
            section.Bind(loaderOptions);

            // Verify the options.
            (loaderOptions as OptionsBase)?.ThrowIfInvalid();

            // Trim the strategy name, just in case.
            var strategyName = loaderOptions.Strategy.Trim();

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

            try
            {
                // Should we try to load an assembly for the repository strategy?
                if (false == string.IsNullOrEmpty(loaderOptions.Assembly))
                {
                    // Load the assembly.
                    _ = Assembly.Load(loaderOptions.Assembly);

                    // If an assembly was specified then we should be able to
                    // make use of the white list to significantly improve 
                    //   the runtime of the search operation we're about to
                    //   perform.
                    assemblyWhiteList = assemblyWhiteList.Length > 0
                        ? $"{assemblyWhiteList}, {loaderOptions.Assembly}"
                        : assemblyWhiteList;
                }
            }
            catch (Exception ex)
            {
                // Provide better context for the error.
                throw new BusinessException(
                    message: string.Format(
                        Resources.ServiceCollectionExtensions_NoLoadAssembly,
                        loaderOptions.Assembly
                        ),
                    innerException: ex
                    );
            }

            // Format the name of a target extension method.
            var methodName = $"Add{strategyName}Repositories";

            // Look for specified extension method on the TBuilder type.
            var methods = AppDomain.CurrentDomain.ExtensionMethods(
                typeof(TBuilder),
                methodName,
                new Type[] { typeof(IConfiguration) },
                assemblyWhiteList,
                assemblyBlackList
                );

            // Did we find it?
            if (methods.Any())
            {
                // Drill down into the appropriate sub-section.
                var subSection = section.GetSection(
                    strategyName
                    );

                // We'll use the first matching method.
                var method = methods.First();

                // Create the builder.
                var builder = new TBuilder()
                {
                    ServiceCollection = serviceCollection
                };

                // Invoke the extension method.
                method.Invoke(
                    null,
                    new object[] { builder, subSection }
                    );
            }
            else
            {
                // If we get here we found the assembly but couldn't find a matching
                //   extension method!

                // Panic!
                throw new BusinessException(
                    message: string.Format(
                        Resources.ServiceCollectionExtensions_MethodNotFound2,
                        methodName,
                        typeof(TBuilder).Name
                        )
                    );
            }

            // Return the service collection.
            return serviceCollection;
        }

        #endregion
    }
}
