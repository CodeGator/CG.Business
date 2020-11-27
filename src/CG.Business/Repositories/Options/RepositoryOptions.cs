using CG.Business.Properties;
using CG.Options;
using System.ComponentModel.DataAnnotations;

namespace CG.Business.Repositories.Options
{
    /// <summary>
    /// This class represents configuration options for a repository.
    /// </summary>
    public class RepositoryOptions : OptionsBase
    {
        // *******************************************************************
        // Constants.
        // *******************************************************************

        #region Constants

        /// <summary>
        /// This constant represents the configuration section root for repository 
        /// options. This means options for repositories should be bound at a 
        /// point just above the Repositories node, in the configuration.
        /// </summary>
        public const string SectionName = "Repositories";

        #endregion

        // *******************************************************************
        // Properties.
        // *******************************************************************

        #region Properties

        /// <summary>
        /// This property contains the name of the configured repository strategy.
        /// </summary>
        [Required(ErrorMessageResourceName = "RepositoryOptions_Strategy", 
                  ErrorMessageResourceType = typeof(Resources) )]
        public string Strategy { get; set; }

        /// <summary>
        /// This property contains the optional name of the assembly that contains 
        /// the repository strategy.
        /// </summary>
        public string Assembly { get; set; }

        #endregion
    }
}
