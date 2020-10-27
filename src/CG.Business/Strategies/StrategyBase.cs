using CG.Business.Strategies.Options;
using CG.Validations;
using Microsoft.Extensions.Options;

namespace CG.Business.Strategies
{
    /// <summary>
    /// This class represents a base implementation of the <see cref="IStrategy"/>
    /// inteface.
    /// </summary>
    public abstract class StrategyBase :
        DisposableBase,
        IStrategy
    {

    }

    /// <summary>
    /// This class represents a base implementation of the <see cref="IStrategy"/>
    /// interface.
    /// </summary>
    /// <typeparam name="TOptions">The type of associated options.</typeparam>
    public abstract class StrategyBase<TOptions> :
        StrategyBase,
        IStrategy
        where TOptions : StrategyOptions, new()
    {
        // *******************************************************************
        // Properties.
        // *******************************************************************

        #region Properties

        /// <summary>
        /// This property contains options for the strategy.
        /// </summary>
        protected IOptions<TOptions> Options { get; }

        #endregion

        // *******************************************************************
        // Constructors.
        // *******************************************************************

        #region Constructors

        /// <summary>
        /// This constructor creates a new instance of the <see cref="StrategyBase{TOptions}"/>
        /// class.
        /// </summary>
        /// <param name="options">The options to use with the strategy.</param>
        public StrategyBase(
            IOptions<TOptions> options
            )
        {
            // Validate the parameters before attempting to use them.
            Guard.Instance().ThrowIfNull(options, nameof(options));

            // Save the referrence.
            Options = options;
        }

        #endregion
    }
}
