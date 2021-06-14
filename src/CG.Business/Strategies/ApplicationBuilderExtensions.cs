using CG.Business;
using CG.Business.Properties;
using CG.Configuration;
using CG.Reflection;
using CG.Validations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Microsoft.AspNetCore.Builder
{
    /// <summary>
    /// This class contains extension methods related to the <see cref="IApplicationBuilder"/>
    /// type, for registering types related to strategies.
    /// </summary>
    public static partial class ApplicationBuilderExtensions
    {
        // *******************************************************************
        // Public methods.
        // *******************************************************************

        #region Public methods

        /// <summary>
        /// This method reads the specified configuration for information to 
        /// dynamically locate (possibly by loading an external assembly), and 
        /// then invoke, one or more user supplied extension methods, for the 
        /// purpose of calling startup logic for strategy related abstractions. 
        /// </summary>
        /// <param name="applicationBuilder">The application builder to use for 
        /// the operation.</param>
        /// <param name="hostEnvironment">The hosting environment to use for the operation.</param>
        /// <param name="configuration">The configuration section to use for the operation.</param>
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
            IHostEnvironment hostEnvironment,
            IConfiguration configuration,
            string assemblyWhiteList = "",
            string assemblyBlackList = "Microsoft*, System*, mscorlib, netstandard"
            )
        {
            // Validate the parameters before attempting to use them.
            Guard.Instance().ThrowIfNull(applicationBuilder, nameof(applicationBuilder))
                .ThrowIfNull(hostEnvironment, nameof(hostEnvironment))
                .ThrowIfNull(configuration, nameof(configuration));


            // First, we need to ensure the 'Selected' field is actually there.
            if (configuration.FieldIsMissing("Selected"))
            {
                // Throw an error with (hopefully) better context.
                throw new ConfigurationException(
                    message: string.Format(
                        Resources.MissingSelectedField,
                        nameof(UseStrategies),
                        configuration.GetPath()
                        )
                    );
            }

            // The 'Selected' field, in the configuration, can be either a single 
            //   value or an array of values. We'll need to determine what we're
            //   looking at before we try to process anything.

            // Is 'Selected' an array?
            if (configuration.FieldIsArray("Selected"))
            {
                // If we get here then the configuration holds an array.

                // Try to get the list of selections.
                if (false == configuration.TryGetAsList<string>("Selected", out var list))
                {
                    // Panic!
                    throw new ConfigurationException(
                        message: string.Format(
                            Resources.MalformedSelectionArray,
                            nameof(UseStrategies),
                            configuration.GetPath()
                            )
                        );
                }

                // Loop and process each selection in the array.
                var errors = new List<Exception>();
                foreach (var strategyName in list)
                {
                    try
                    {
                        // Process the selection.
                        applicationBuilder.ProcessStrategySection(
                            hostEnvironment,
                            strategyName,
                            configuration,
                            assemblyWhiteList,
                            assemblyBlackList
                            );
                    }
                    catch (Exception ex)
                    {
                        errors.Add(ex);
                    }
                }

                // Did we fail at any point?
                if (errors.Any())
                {
                    // Throw an error with (hopefully) better context.
                    throw new AggregateException(
                        message: string.Format(
                            Resources.AggregatedFail,
                            nameof(UseStrategies),
                            configuration.GetPath()
                            ),
                        innerExceptions: errors
                        );
                }
            }
            else
            {
                // If we get here then the configuration holds a single value.

                try
                {
                    // Process the selection.
                    applicationBuilder.ProcessStrategySection(
                        hostEnvironment,
                        configuration["Selected"],
                        configuration,
                        assemblyWhiteList,
                        assemblyBlackList
                        );
                }
                catch (Exception)
                {
                    // Throw an error with (hopefully) better context.
                    throw new ConfigurationException(
                        message: string.Format(
                            Resources.SingleFail,
                            nameof(UseStrategies),
                            configuration["Selected"],
                            configuration.GetPath()
                            )
                        );
                }
            }

            // Return the application builder.
            return applicationBuilder;
        }

        #endregion

        // *******************************************************************
        // Private methods.
        // *******************************************************************

        #region Private methods

        /// <summary>
        /// This method is called, internally, to start the specified repository
        /// strategy.
        /// </summary>
        /// <param name="applicationBuilder">The application builder to use 
        /// for the operation.</param>
        /// <param name="hostEnvironment">The host environment to use for the 
        /// operation.</param>
        /// <param name="strategyName">The strategy name to use for the operation.</param>
        /// <param name="configuration">The configuration to use for the 
        /// operation.</param>
        /// <param name="assemblyWhiteList">An optional white list for filtering
        /// the list of assemblies that are searched during this operation.</param>
        /// <param name="assemblyBlackList">An optional black list for filtering
        /// the list of assemblies that are searched during this operation.</param>
        /// <returns>the value of the <paramref name="applicationBuilder"/>
        /// parameter, for chaining calls together.</returns>
        internal static IApplicationBuilder ProcessStrategySection(
           this IApplicationBuilder applicationBuilder,
           IHostEnvironment hostEnvironment,
           string strategyName,
           IConfiguration configuration,
           string assemblyWhiteList,
           string assemblyBlackList
           )
        {
            // Validate the parameters before attempting to use them.
            Guard.Instance().ThrowIfNull(applicationBuilder, nameof(applicationBuilder))
                .ThrowIfNull(configuration, nameof(configuration));

            // Strategy name is a configuration parameter, not just a programming
            //   argument to a method call, so, we'll check it here because the programmer
            //   might have supplied the argument while the configuration itself might be 
            //   missing any actual information.

            // Trim the strategy name, just in case.
            strategyName = strategyName.Trim();

            // Should never happen, but, pffft, check it anyway
            if (string.IsNullOrEmpty(strategyName))
            {
                // Panic!
                throw new ConfigurationException(
                    message: string.Format(
                        Resources.EmptyStrategyName,
                        nameof(ProcessStrategySection)
                        )
                    );
            }

            // Drill down into the appropriate sub-section.
            var subSection = configuration.GetSection(
                strategyName
                );

            // Is the sub-section populated?
            if (subSection.HasChildren())
            {
                // Is the 'AssemblyNameOrPath' field there?
                if (false == subSection.FieldIsMissing("AssemblyNameOrPath"))
                {
                    // Get the value of the field.
                    var assemblyNameOrPath = subSection["AssemblyNameOrPath"].Trim();

                    // Is there anything in the field?
                    if (false == string.IsNullOrEmpty(assemblyNameOrPath))
                    {
                        // If an assembly name/path was specified then we should be able
                        //   to doctor it up a bit and add it to the white list to
                        //   significantly improve the runtime of the search operation
                        //   we're about to perform. We'll pull the actual name from the 
                        //   loaded assembly, in case the caller fat-fingered the filename
                        //   in any way (the upcoming compares are all case sensitive).

                        try
                        {
                            // Should we load the assembly by path, or name?
                            if (assemblyNameOrPath.EndsWith(".dll"))
                            {
                                // Load the assembly.
                                var tempAsm = Assembly.LoadFrom(assemblyNameOrPath);

                                // Add it to the white list.
                                assemblyWhiteList = assemblyWhiteList.Length > 0
                                    ? $"{assemblyWhiteList}, {Path.GetFileNameWithoutExtension(tempAsm.Location)}"
                                    : $"{Path.GetFileNameWithoutExtension(tempAsm.Location)}";
                            }
                            else
                            {
                                // Load the assembly by yname.
                                var tempAsm = Assembly.Load(assemblyNameOrPath);

                                // Add it to the white list.
                                assemblyWhiteList = assemblyWhiteList.Length > 0
                                    ? $"{assemblyWhiteList}, {Path.GetFileNameWithoutExtension(tempAsm.Location)}"
                                    : $"{Path.GetFileNameWithoutExtension(tempAsm.Location)}";
                            }
                        }
                        catch (Exception ex)
                        {
                            // Provide better context for the error.
                            throw new BusinessException(
                                message: string.Format(
                                    Resources.NoLoadAssembly,
                                    assemblyNameOrPath
                                    ),
                                innerException: ex
                                );
                        }
                    }
                }
            }
            else
            {
                // If we get here then the 'Selected' field contains a value that
                //   doesn't correspond to a child section. In itself, that isn't an
                //   error, but, it probably is something we should warn about ...
                // Hmm ... should we pull in an ILogger parameter just for this warning?
            }

            // Format the name of a target extension method.
            var methodName = $"Use{strategyName}Repositories";

            // Look for the specified extension method.
            var methods = AppDomain.CurrentDomain.ExtensionMethods(
                typeof(IApplicationBuilder),
                methodName,
                new Type[] { typeof(IHostEnvironment) },
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
                    new object[]
                    {
                        applicationBuilder,
                        hostEnvironment
                    });
            }
            else
            {
                // Panic!
                throw new BusinessException(
                    message: string.Format(
                        Resources.MethodNotFound,
                        nameof(ProcessStrategySection),
                        methodName,
                        $"looked for parameters: '{nameof(IApplicationBuilder)},{nameof(IHostEnvironment)}'"
                        )
                    );
            }

            // Return the application builder.
            return applicationBuilder;
        }

        #endregion
    }
}
