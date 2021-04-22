using CG.Business.Directors.Options;
using CG.Validations;
using Microsoft.Extensions.Options;

namespace CG.Business.Directors
{
    /// <summary>
    /// This class represents a base implementation of the <see cref="IDirector"/>
    /// inteface.
    /// </summary>
    public abstract class DirectorBase :
        DisposableBase,
        IDirector
    {

    }

    /// <summary>
    /// This class represents a base implementation of the <see cref="IDirector"/>
    /// interface.
    /// </summary>
    /// <typeparam name="TOptions">The type of associated options.</typeparam>
    public abstract class DirectorBase<TOptions> :
        DirectorBase,
        IDirector
        where TOptions : DirectorOptions, new()
    {
        // *******************************************************************
        // Properties.
        // *******************************************************************

        #region Properties

        /// <summary>
        /// This property contains options for the director.
        /// </summary>
        protected IOptions<TOptions> Options { get; }

        #endregion

        // *******************************************************************
        // Constructors.
        // *******************************************************************

        #region Constructors

        /// <summary>
        /// This constructor creates a new instance of the <see cref="DirectorBase{TOptions}"/>
        /// class.
        /// </summary>
        /// <param name="options">The options to use with the director.</param>
        public DirectorBase(
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
