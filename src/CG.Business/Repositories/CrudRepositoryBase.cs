using CG.Business.Models;
using CG.Business.Repositories.Options;
using CG.Validations;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CG.Business.Repositories
{
    /// <summary>
    /// This class is a default implementation of the <see cref="ICrudRepository{TModel, TKey}"/>
    /// interface.
    /// </summary>
    /// <typeparam name="TModel">The model type associated with the repository.</typeparam>
    /// <typeparam name="TKey">The key type associated with the model.</typeparam>
    public abstract class CrudRepositoryBase<TModel, TKey> : 
        RepositoryBase,
        ICrudRepository<TModel, TKey>
        where TModel : ModelBase<TKey>
    {
        // *******************************************************************
        // Public methods.
        // *******************************************************************

        #region Public methods

        /// <summary>
        /// This method returns an <see cref="IQueryable{TModel}"/> object.
        /// </summary>
        /// <returns>An <see cref="IQueryable{TModel}"/> object</returns>
        public abstract IQueryable<TModel> AsQueryable();

        /// <summary>
        /// This method adds a new <typeparamref name="TModel"/> to the 
        /// repository.
        /// </summary>
        /// <param name="model">The model to use for the operation. </param>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>A task to perform the operation, that returns the recently
        /// added <typeparamref name="TModel"/> object.</returns>
        public abstract Task<TModel> AddAsync(
            TModel model,
            CancellationToken cancellationToken = default
            );

        /// <summary>
        /// This method updates a <typeparamref name="TModel"/> in the 
        /// repository.
        /// </summary>
        /// <param name="model">The model to use for the operation. </param>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>A task to perform the operation, that returns the recently
        /// updated <typeparamref name="TModel"/> object.</returns>
        public abstract Task<TModel> UpdateAsync(
            TModel model,
            CancellationToken cancellationToken = default
            );

        /// <summary>
        /// This method deletes a <typeparamref name="TModel"/> from the 
        /// repository.
        /// </summary>
        /// <param name="model">The model to use for the operation.</param>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>A task to perform the operation.</returns>
        public abstract Task DeleteAsync(
            TModel model,
            CancellationToken cancellationToken = default
            );

        #endregion
    }


    /// <summary>
    /// This class is a default implementation of the <see cref="ICrudRepository{TModel, TKey}"/>
    /// interface.
    /// </summary>
    /// <typeparam name="TOptions">The options type associated with the repository.</typeparam>
    /// <typeparam name="TModel">The model type associated with the repository.</typeparam>
    /// <typeparam name="TKey">The key type associated with the model.</typeparam>
    public abstract class CrudRepositoryBase<TOptions, TModel, TKey> : 
        CrudRepositoryBase<TModel, TKey>,
        ICrudRepository<TModel, TKey>
        where TModel : ModelBase<TKey>
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
        /// This constructor creates a new instance of the <see cref="CrudRepositoryBase{TOptions, TModel, TKey}"/>
        /// class.
        /// </summary>
        /// <param name="options">The options to use with the repository.</param>
        protected CrudRepositoryBase(
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
    /// This class is an implementation of the <see cref="ICrudRepository{TModel, TKey}"/>
    /// interface that adds an extra type parameter for an entity.
    /// </summary>
    /// <typeparam name="TOptions">The options type associated with the repository.</typeparam>
    /// <typeparam name="TEntity">The entity type associated with the repository.</typeparam>
    /// <typeparam name="TModel">The model type associated with the repository.</typeparam>
    /// <typeparam name="TKey">The key type associated with the model.</typeparam>
    public abstract class CrudRepositoryBase<TOptions, TEntity, TModel, TKey> :
        CrudRepositoryBase<TOptions, TModel, TKey>,
        ICrudRepository<TModel, TKey>
        where TEntity : class
        where TModel : ModelBase<TKey>
        where TOptions : IOptions<RepositoryOptions>
    {
        // *******************************************************************
        // Constructors.
        // *******************************************************************

        #region Constructors

        /// <summary>
        /// This constructor creates a new instance of the <see cref="CrudRepositoryBase{TOptions, TEntity, TModel, TKey}"/>
        /// class.
        /// </summary>
        /// <param name="options">The options to use with the repository.</param>
        protected CrudRepositoryBase(
            TOptions options
            ) : base(options)
        {

        }

        #endregion

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
}
