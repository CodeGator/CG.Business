using CG.Business.Repositories.Options;
using CG.Validations;
using Microsoft.Extensions.Options;

namespace CG.Business.Repositories
{
    /// <summary>
    /// This class represents a base implementation of the <see cref="IRepository"/>
    /// inteface.
    /// </summary>
    public abstract class RepositoryBase :
        DisposableBase,
        IRepository
    {

    }


    /// <summary>
    /// This class represents a base implementation of the <see cref="IRepository"/>
    /// interface.
    /// </summary>
    /// <typeparam name="TOptions">The type of associated options.</typeparam>
    public abstract class RepositoryBase<TOptions> :
        RepositoryBase,
        IRepository
        where TOptions : IOptions<RepositoryOptions>
    {
        // *******************************************************************
        // Properties.
        // *******************************************************************

        #region Properties

        /// <summary>
        /// This property contains options for the repository.
        /// </summary>
        protected TOptions Options { get; }

        #endregion

        // *******************************************************************
        // Constructors.
        // *******************************************************************

        #region Constructors

        /// <summary>
        /// This constructor creates a new instance of the <see cref="RepositoryBase{TOptions}"/>
        /// class.
        /// </summary>
        /// <param name="options">The options to use with the repository.</param>
        public RepositoryBase(
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
