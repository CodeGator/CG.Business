using CG.Business.Stores.Options;
using CG.Validations;
using Microsoft.Extensions.Options;

namespace CG.Business.Stores
{
    /// <summary>
    /// This class represents a base implementation of the <see cref="IStore"/>
    /// interface.
    /// </summary>
    public abstract class StoreBase : DisposableBase, IStore
    {

    }


    /// <summary>
    /// This class represents a base implementation of the <see cref="IStore"/>
    /// interface.
    /// </summary>
    /// <typeparam name="TOptions">The type of associated options.</typeparam>
    public abstract class StoreBase<TOptions> : StoreBase, IStore
        where TOptions : StoreOptions, new()
    {
        // *******************************************************************
        // Properties.
        // *******************************************************************

        #region Properties

        /// <summary>
        /// This property contains options for the manager.
        /// </summary>
        protected IOptions<TOptions> Options { get; }

        #endregion

        // *******************************************************************
        // Constructors.
        // *******************************************************************

        #region Constructors

        /// <summary>
        /// This constructor creates a new instance of the <see cref="StoreBase{TOptions}"/>
        /// class.
        /// </summary>
        /// <param name="options">The options to use with the store.</param>
        public StoreBase(
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
