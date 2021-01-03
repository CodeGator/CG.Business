﻿using CG.Business;
using CG.Business.Options;
using CG.Business.Properties;
using CG.Configuration;
using CG.DataAnnotations;
using CG.Reflection;
using CG.Validations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

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
        /// required to run the configured strategy type(s).
        /// </summary>
        /// <param name="applicationBuilder">The application builder to use for 
        /// the operation.</param>
        /// <param name="hostEnvironment">The host environment to use for the operation.</param>
        /// <param name="configurationSection">The configuration sub-section to use 
        /// for the operation. </param>
        /// <param name="assemblyBlackList">An optional black list for filtering
        /// the list of assemblies that are searched during this operation.</param>
        /// <param name="assemblyWhiteList">An optional white list for filtering
        /// the list of assemblies that are searched during this operation.</param>
        /// <returns>The value of the <paramref name="applicationBuilder"/>
        /// parameter, for chaining calls together.</returns>
        /// <exception cref="ArgumentException">This exception is thrown whenever one
        /// or more arguments are invalid, or missing.</exception>
        /// <exception cref="ConfigurationException">This exception is thrown whenever
        /// the method failes to locate appropriate configuration sections and/or settings
        /// at runtime.</exception>
        /// <exception cref="BusinessException">This exception is thrown whenever the 
        /// method failes to locate an appropriate extension method to call, or when that
        /// call fails.</exception>
        public static IApplicationBuilder UseStrategies(
            this IApplicationBuilder applicationBuilder,
            IWebHostEnvironment hostEnvironment,
            string configurationSection,
            string assemblyWhiteList = "",
            string assemblyBlackList = "Microsoft*, System*, mscorlib, netstandard"
            ) 
        {
            // Validate the parameters before attempting to use them.
            Guard.Instance().ThrowIfNull(applicationBuilder, nameof(applicationBuilder))
                .ThrowIfNull(hostEnvironment, nameof(hostEnvironment));

            // Get the configuration root.
            var configuration = applicationBuilder.ApplicationServices
                    .GetRequiredService<IConfiguration>();

            // Navigate to the desired section.
            var section = configuration.GetSection(
                configurationSection
                );

            // Create the loader options.
            var loaderOptions = new LoaderOptions();

            // Bind the loader options to the configuration.
            section.Bind(loaderOptions);

            // Verify the loader options.
            if (false == loaderOptions.IsValid())
            {
                // Throw an error with (hopefully) better context.
                throw new ConfigurationException(
                    message: string.Format(
                        Resources.InvalidLoaderSection,
                        nameof(UseStrategies),
                        configurationSection
                        )
                    );
            }

            // Trim the strategy name, just in case.
            var strategyName = loaderOptions.Name.Trim();

            // Should never happen, but, pffft, check it anyway
            if (string.IsNullOrEmpty(strategyName))
            {
                // Panic!
                throw new ConfigurationException(
                    message: string.Format(
                        Resources.EmptyStrategyName,
                        nameof(UseStrategies)
                        )
                    );
            }

            try
            {
                // Should we try to load an assembly for the repository strategy?
                if (false == string.IsNullOrEmpty(loaderOptions.AssemblyNameOrPath))
                {
                    // Should we load the assembly by path, or name?
                    if (loaderOptions.AssemblyNameOrPath.EndsWith(".dll"))
                    {
                        // Load the assembly.
                        _ = Assembly.LoadFrom(
                            loaderOptions.AssemblyNameOrPath
                            );

                        // If an assembly path was specified then we should be able
                        //   to doctor that path up a bit and add it to the white list
                        //   to significantly improve the runtime of the search operation
                        //   we're about to perform.
                        assemblyWhiteList = assemblyWhiteList.Length > 0
                            ? $"{assemblyWhiteList}, {Path.GetFileNameWithoutExtension(loaderOptions.AssemblyNameOrPath)}"
                            : assemblyWhiteList;
                    }
                    else
                    {
                        // Load the assembly by yname.
                        _ = Assembly.Load(
                            loaderOptions.AssemblyNameOrPath
                            );

                        // If an assembly name was specified then we should be able to
                        //   make use of the white list to significantly improve the
                        //   runtime of the search operation we're about to perform.
                        assemblyWhiteList = assemblyWhiteList.Length > 0
                            ? $"{assemblyWhiteList}, {loaderOptions.AssemblyNameOrPath}"
                            : assemblyWhiteList;
                    }
                }
            }
            catch (Exception ex)
            {
                // Provide better context for the error.
                throw new BusinessException(
                    message: string.Format(
                        Resources.NoLoadAssembly,
                        loaderOptions.AssemblyNameOrPath
                        ),
                    innerException: ex
                    );
            }

            // Format the name of a target extension method.
            var methodName = $"Use{strategyName}Strategies";

            // Look for specified extension method.
            var methods = AppDomain.CurrentDomain.ExtensionMethods(
                typeof(IApplicationBuilder),
                methodName,
                new Type[] { typeof(IWebHostEnvironment), typeof(string) },
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
                    new object[] { applicationBuilder, hostEnvironment, configurationSection }
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
                        nameof(UseRepositories),
                        methodName,
                        $"{nameof(IApplicationBuilder)},{nameof(IWebHostEnvironment)}, {nameof(String)}"
                        )
                    );
            }

            // Return the application builder.
            return applicationBuilder;
        }

        #endregion
    }
}