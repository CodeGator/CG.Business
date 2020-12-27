using CG.Business;
using CG.Business.Options;
using CG.Business.Properties;
using CG.DataAnnotations;
using CG.Options;
using CG.Reflection;
using CG.Validations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Microsoft.AspNetCore.Builder
{
    /// <summary>
    /// This class contains extension methods related to the <see cref="IApplicationBuilder"/>
    /// type.
    /// </summary>
    public static partial class ApplicationBuilderExtensions
    {
        // *******************************************************************
        // Public methods.
        // *******************************************************************

        #region Public methods

        /// <summary>
        /// This method reads the configuration for a <see cref="LoaderOptions"/>
        /// compatible section. It then uses that information to dynamically locate,
        /// and then invoke, an extension method that wires up any startup logic
        /// required to run the configured repository type(s).
        /// </summary>
        /// <param name="applicationBuilder">The application builder to use for 
        /// the operation.</param>
        /// <param name="hostEnvironment">The host environment to use for the operation.</param>
        /// <param name="assemblyBlackList">An optional black list for filtering
        /// the list of assemblies that are searched during this operation.</param>
        /// <param name="assemblyWhiteList">An optional white list for filtering
        /// the list of assemblies that are searched during this operation.</param>
        /// <returns>The value of the <paramref name="applicationBuilder"/>
        /// parameter, for chaining calls together.</returns>
        /// <exception cref="ArgumentException">This exception is thrown whenever one
        /// or more arguments are invalid, or missing.</exception>
        public static IApplicationBuilder UseRepositories(
            this IApplicationBuilder applicationBuilder,
            IWebHostEnvironment hostEnvironment,
            string assemblyWhiteList = "",
            string assemblyBlackList = "Microsoft*, System*, mscorlib, netstandard"
            ) 
        {
            // Validate the parameters before attempting to use them.
            Guard.Instance().ThrowIfNull(applicationBuilder, nameof(applicationBuilder))
                .ThrowIfNull(hostEnvironment, nameof(hostEnvironment));

            // Get the configuration.
            var configuration = applicationBuilder.ApplicationServices.GetRequiredService<
                IConfiguration
                >();

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
                        Resources.EmptyStrategyName,
                        nameof(UseRepositories)
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
                        Resources.NoLoadAssembly,
                        loaderOptions.Assembly
                        ),
                    innerException: ex
                    );
            }

            // Format the name of a target extension method.
            var methodName = $"Use{strategyName}Repositories";

            // Look for specified extension method.
            var methods = AppDomain.CurrentDomain.ExtensionMethods(
                typeof(IApplicationBuilder),
                methodName,
                new Type[] { typeof(IWebHostEnvironment) },
                assemblyWhiteList,
                assemblyBlackList
                );

            // Did we find it?
            if (methods.Any())
            {
                // We'll use the first matching method.
                var method = methods.First();

                // Invoke the extension method.
                method.Invoke(
                    null,
                    new object[] { applicationBuilder, hostEnvironment }
                    );
            }
            else
            {
                // If we get here we found the assembly but couldn't find a matching
                //   extension method!

                // Panic!
                throw new BusinessException(
                    message: string.Format(
                        Resources.MethodNotFound,
                        methodName
                        )
                    );
            }

            // Return the application builder.
            return applicationBuilder;
        }

        #endregion
    }
}
