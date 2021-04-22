using CG.Business.Managers.Options;
using CG.Validations;
using Microsoft.Extensions.Options;

namespace CG.Business.Managers
{
    /// <summary>
    /// This class represents a base implementation of the <see cref="IManager"/>
    /// inteface.
    /// </summary>
    public abstract class ManagerBase :
        DisposableBase,
        IManager
    {

    }

    /// <summary>
    /// This class represents a base implementation of the <see cref="IManager"/>
    /// interface.
    /// </summary>
    /// <typeparam name="TOptions">The type of associated options.</typeparam>
    public abstract class ManagerBase<TOptions> :
        ManagerBase,
        IManager
        where TOptions : ManagerOptions, new()
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
        /// This constructor creates a new instance of the <see cref="ManagerBase{TOptions}"/>
        /// class.
        /// </summary>
        /// <param name="options">The options to use with the manager.</param>
        public ManagerBase(
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
