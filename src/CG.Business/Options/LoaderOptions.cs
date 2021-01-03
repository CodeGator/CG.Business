using CG.Business.Properties;
using CG.Options;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.ComponentModel.DataAnnotations;

namespace CG.Business.Options
{
    /// <summary>
    /// This class represents configuration settings for a strategy loader.
    /// </summary>
    public sealed class LoaderOptions : OptionsBase
    {
        // *******************************************************************
        // Properties.
        // *******************************************************************

        #region Properties

        /// <summary>
        /// This property contains the name of a configured loader strategy.
        /// </summary>
        [Required(ErrorMessageResourceName = "LoaderOptions_Name",
                  ErrorMessageResourceType = typeof(Resources))]
        public string Name { get; set; }

        /// <summary>
        /// This property contains the optional name of, or path to, an assembly 
        /// that contains one or more extension method(s), for the loader strategy.
        /// </summary>
        public string AssemblyNameOrPath { get; set; }

        /// <summary>
        /// This property contains a service lifetime hint, which is passed 
        /// down the any internal abstractions, and helps to ensure that everything
        /// in the object graph has the same lifetime rules applied.
        /// </summary>
        public ServiceLifetime ServiceLifetime { get; set; }

        #endregion

        // *******************************************************************
        // Constructors.
        // *******************************************************************

        #region Constructors

        /// <summary>
        /// This constructor creates a new instance of the <see cref="LoaderOptions"/>
        /// class.
        /// </summary>
        public LoaderOptions()
        {
            // Setup defaults.
            ServiceLifetime = ServiceLifetime.Singleton;
        }

        #endregion
    }
}
