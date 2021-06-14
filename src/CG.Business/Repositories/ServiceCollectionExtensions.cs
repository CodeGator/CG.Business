using CG.Business;
using CG.Business.Properties;
using CG.Configuration;
using CG.Reflection;
using CG.Validations;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// This class contains extension methods related to the <see cref="IServiceCollection"/>
    /// type, for registering types related to repositories.
    /// </summary>
    public static partial class ServiceCollectionExtensions
    {
        // *******************************************************************
        // Public methods.
        // *******************************************************************

        #region Public methods

        /// <summary>
        /// This method reads the specified configuration for information to 
        /// dynamically locate (possibly by loading an external assembly), and 
        /// then invoke, one or more user supplied extension methods, for the 
        /// purpose of registering repository related abstractions with the 
        /// supplied service collection. 
        /// </summary>
        /// <param name="serviceCollection">The service collection to use 
        /// for the operation.</param>
        /// <param name="configuration">The configuration to use for the 
        /// operation.</param>
        /// <param name="serviceLifetime">The service lifetime to use for the operation.</param>
        /// <param name="assemblyWhiteList">An optional white list for filtering
        /// the list of assemblies that are searched during this operation.</param>
        /// <param name="assemblyBlackList">An optional black list for filtering
        /// the list of assemblies that are searched during this operation.</param>
        /// <returns>the value of the <paramref name="serviceCollection"/>
        /// parameter, for chaining calls together.</returns>
        /// <exception cref="ArgumentException">This exception is thrown whenever
        /// one ore more of the parameters is missing, or invalid.</exception>
        /// <exception cref="ConfigurationException">This exception is thrown whenever
        /// the method failes to locate appropriate configuration sections and/or settings
        /// at runtime.</exception>
        /// <exception cref="BusinessException">This exception is thrown whenever the 
        /// method failes to locate an appropriate extension method to call, or when that
        /// call fails.</exception>
        /// <remarks>
        /// <para>
        /// This method expects a field named 'Selected' to be in the configuration at whatever
        /// location the method is pointed to, when it is called. The method will read that 
        /// field, then look for a matching sub-section, in order to register the appropriate 
        /// objects with, in the service collection. 
        /// </para>
        /// <para>
        /// The 'Selected' field can either be a single string, or an array of strings. 
        /// </para>
        /// <para>
        /// The sub sections mention before are optional, but if they are supplied, they MUST 
        /// have a name that matches whatever is in the 'Selected' field. The structure of 
        /// the sub sections is completely up to the caller. The appropriate section will be 
        /// read into an <see cref="IConfiguration"/> object and passed, as an argument, to 
        /// the specified extension method.
        /// </para>
        /// </remarks>
        public static IServiceCollection AddRepositories(
            this IServiceCollection serviceCollection,
            IConfiguration configuration,
            ServiceLifetime serviceLifetime = ServiceLifetime.Scoped,
            string assemblyWhiteList = "",
            string assemblyBlackList = "Microsoft*, System*, mscorlib, netstandard"
            )
        {
            // Validate the parameters before attempting to use them.
            Guard.Instance().ThrowIfNull(serviceCollection, nameof(serviceCollection))
                .ThrowIfNull(configuration, nameof(configuration));

            // First, we need to ensure the 'Selected' field is actually there.
            if (configuration.FieldIsMissing("Selected"))
            {
                // Throw an error with (hopefully) better context.
                throw new ConfigurationException(
                    message: string.Format(
                        Resources.MissingSelectedField,
                        nameof(AddRepositories),
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
                            nameof(AddRepositories),
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
                        serviceCollection.ProcessRepositorySection(
                            strategyName,
                            configuration,
                            serviceLifetime,
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
                            nameof(AddRepositories),
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
                    serviceCollection.ProcessRepositorySection(
                        configuration["Selected"],
                        configuration,
                        serviceLifetime,
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
                            nameof(AddRepositories),
                            configuration["Selected"],
                            configuration.GetPath()
                            )
                        );
                }                
            }

            // Return the service collection.
            return serviceCollection;
        }

        #endregion

        // *******************************************************************
        // Private methods.
        // *******************************************************************

        #region Private methods

        /// <summary>
        /// This method is called, internally, to register the specified repository
        /// section.
        /// </summary>
        /// <param name="serviceCollection">The service collection to use 
        /// for the operation.</param>
        /// <param name="strategyName">The strategy name to use for the operation.</param>
        /// <param name="configuration">The configuration to use for the 
        /// operation.</param>
        /// <param name="serviceLifetime">The service lifetime to use for the operation.</param>
        /// <param name="assemblyWhiteList">An optional white list for filtering
        /// the list of assemblies that are searched during this operation.</param>
        /// <param name="assemblyBlackList">An optional black list for filtering
        /// the list of assemblies that are searched during this operation.</param>
        /// <returns>the value of the <paramref name="serviceCollection"/>
        /// parameter, for chaining calls together.</returns>
        internal static IServiceCollection ProcessRepositorySection(
           this IServiceCollection serviceCollection,
           string strategyName,
           IConfiguration configuration,
           ServiceLifetime serviceLifetime,
           string assemblyWhiteList,
           string assemblyBlackList
           )
        {
            // Validate the parameters before attempting to use them.
            Guard.Instance().ThrowIfNull(serviceCollection, nameof(serviceCollection))
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
                        nameof(ProcessRepositorySection)
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
            var methodName = $"Add{strategyName}Repositories";

            // Our convention is that the specified extension method can appear in
            //   two different flavors, one with a trailing ServiceLifetime parameter,
            //   and the other without. We'll look for both now.

            // Look for the specified extension method.
            var methods = AppDomain.CurrentDomain.ExtensionMethods(
                typeof(IServiceCollection),
                methodName,
                new Type[] { typeof(IConfiguration), typeof(ServiceLifetime) },
                assemblyWhiteList,
                assemblyBlackList
                );

            // Did we find it on the first attempt?
            if (methods.Any())
            {
                // We'll use the first matching method.
                var method = methods.First();

                // Invoke the extension method.
                method.Invoke(
                    null,
                    new object[]
                    {
                        serviceCollection,
                        subSection,
                        serviceLifetime
                    });
            }
            else
            {
                // If we get here then we failed to find a 3 parameter version of the
                //   specified extension method. So we'll now look for a two parameter
                //   version, in case that exists.

                // Look for specified extension method.
                methods = AppDomain.CurrentDomain.ExtensionMethods(
                    typeof(IServiceCollection),
                    methodName,
                    new Type[] { typeof(IConfiguration) },
                    assemblyWhiteList,
                    assemblyBlackList
                    );

                // Did we find it on the second attempt?
                if (methods.Any())
                {
                    // We'll use the first matching method.
                    var method = methods.First();

                    // Invoke the extension method.
                    method.Invoke(
                        null,
                        new object[]
                        {
                            serviceCollection,
                            subSection
                        });
                }
                else
                {
                    // If we get here we couldn't find the specified extension method
                    //   in any combination of parameter(s).

                    // Panic!
                    throw new BusinessException(
                        message: string.Format(
                            Resources.MethodNotFound,
                            nameof(ProcessRepositorySection),
                            methodName,
                            $"looked for variant 1: '{nameof(IServiceCollection)},{nameof(IConfiguration)},{nameof(ServiceLifetime)}' " +
                            $"AND variant 2: '{nameof(IServiceCollection)},{nameof(IConfiguration)}' "
                            )
                        );
                }
            }

            // Return the service collection.
            return serviceCollection;
        }

        #endregion
    }
}
