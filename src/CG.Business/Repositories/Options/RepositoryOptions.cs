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
        /// This constant represents the configuration section for the current
        /// repository strategy.
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
        [Required]
        public string Strategy { get; set; }

        /// <summary>
        /// This property contains the optional name of the assembly that contains 
        /// the repository strategy.
        /// </summary>
        public string Assembly { get; set; }

        #endregion
    }
}
