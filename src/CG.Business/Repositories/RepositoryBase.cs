using CG.Business.Models;
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
    /// <typeparam name="TEntity">The entity type associated with the repository.</typeparam>
    /// <typeparam name="TModel">The model type associated with the repository.</typeparam>
    public abstract class RepositoryBase<TEntity, TModel> :
        RepositoryBase,
        IRepository
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


    /// <summary>
    /// This class represents a base implementation of the <see cref="IRepository"/>
    /// interface.
    /// </summary>
    /// <typeparam name="TOptions">The type of associated options.</typeparam>
    /// <typeparam name="TEntity">The entity type associated with the repository.</typeparam>
    /// <typeparam name="TModel">The model type associated with the repository.</typeparam>
    public abstract class RepositoryBase<TOptions, TEntity, TModel> :
        RepositoryBase<TEntity, TModel>,
        IRepository
        where TOptions : IOptions<RepositoryOptions>
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
