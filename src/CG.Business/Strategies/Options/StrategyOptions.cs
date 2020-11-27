using CG.Business.Properties;
using CG.Options;
using System.ComponentModel.DataAnnotations;

namespace CG.Business.Strategies.Options
{
    /// <summary>
    /// This class represents configuration options for a strategy.
    /// </summary>
    public class StrategyOptions : OptionsBase
    {
        // *******************************************************************
        // Constants.
        // *******************************************************************

        #region Constants

        /// <summary>
        /// This constant represents the configuration section root for strategy 
        /// options. This means options for strategies should be bound at a 
        /// point just above the Strategies node, in the configuration.
        /// </summary>
        public const string SectionName = "Strategies";

        #endregion

        // *******************************************************************
        // Properties.
        // *******************************************************************

        #region Properties

        /// <summary>
        /// This property contains the name of the configured strategy.
        /// </summary>
        [Required(ErrorMessageResourceName = "StrategyOptions_Strategy",
                  ErrorMessageResourceType = typeof(Resources))]
        public string Strategy { get; set; }

        /// <summary>
        /// This property contains the optional name of the assembly that contains 
        /// the strategy.
        /// </summary>
        public string Assembly { get; set; }

        #endregion
    }
}
