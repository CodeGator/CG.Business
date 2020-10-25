using CG.Business.Strategies.Options;
using CG.Business.Models;
using CG.Validations;

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
    /// <typeparam name="TEntity">The entity type associated with the strategy.</typeparam>
    /// <typeparam name="TModel">The model type associated with the strategy.</typeparam>
    public abstract class StrategyBase<TEntity, TModel> :
        StrategyBase,
        IStrategy
        where TEntity : class
        where TModel : ModelBase
    {
        // *******************************************************************
        // Protected methods.
        // *******************************************************************

        #region Protected methods

        /// <summary>
        /// This method is called to convert a model to an entity.
        /// </summary>
        /// <param name="source">The object to be converted.</param>
        /// <returns>A converted entity.</returns>
        protected abstract TEntity ToEntity(TModel source);

        /// <summary>
        /// This method is called to convert an entity to a model.
        /// </summary>
        /// <param name="source">The object to be converted.</param>
        /// <returns>A converted model.</returns>
        protected abstract TModel ToModel(TEntity source);

        #endregion
    }


    /// <summary>
    /// This class represents a base implementation of the <see cref="IStrategy"/>
    /// interface.
    /// </summary>
    /// <typeparam name="TOptions">The type of associated options.</typeparam>
    public abstract class StrategyBase<TOptions> :
        StrategyBase,
        IStrategy
        where TOptions : StrategyOptions
    {
        // *******************************************************************
        // Properties.
        // *******************************************************************

        #region Properties

        /// <summary>
        /// This property contains options for the strategy.
        /// </summary>
        protected TOptions Options { get; }

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
            TOptions options
            )
        {
            // Validate the parameters before attempting to use them.
            Guard.Instance().ThrowIfNull(options, nameof(options));

            // Save the referrence.
            Options = options;
        }

        #endregion
    }


    /// <summary>
    /// This class represents a base implementation of the <see cref="IStrategy"/>
    /// interface.
    /// </summary>
    /// <typeparam name="TOptions">The type of associated options.</typeparam>
    /// <typeparam name="TEntity">The entity type associated with the strategy.</typeparam>
    /// <typeparam name="TModel">The model type associated with the strategy.</typeparam>
    public abstract class StrategyBase<TOptions, TEntity, TModel> :
        StrategyBase<TEntity, TModel>,
        IStrategy
        where TOptions : StrategyOptions
        where TEntity : class
        where TModel : ModelBase
    {
        // *******************************************************************
        // Properties.
        // *******************************************************************

        #region Properties

        /// <summary>
        /// This property contains options for the manager.
        /// </summary>
        protected TOptions Options { get; }

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
            TOptions options
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
