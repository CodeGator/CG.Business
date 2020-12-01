using CG.Business.Models;
using CG.Business.Repositories;
using System.Text.Json;
using CG.Validations;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CG.Business.Properties;
using CG.Business.Stores.Options;

namespace CG.Business.Stores
{
    /// <summary>
    /// This class represents a base implementation of the <see cref="ICrudStore{TModel, TKey}"/>
    /// interface.
    /// </summary>
    /// <typeparam name="TModel">The model type associated with the repository.</typeparam>
    /// <typeparam name="TKey">The key type associated with the model.</typeparam>
    /// <typeparam name="TRepository">The type of associated repository.</typeparam>
    public class CrudStoreBase<TModel, TKey, TRepository> : 
        StoreBase, 
        ICrudStore<TModel, TKey>
        where TModel : class, IModel<TKey>
        where TRepository : class, ICrudRepository<TModel, TKey>
        where TKey : new()
    {
        // *******************************************************************
        // Properties.
        // *******************************************************************

        #region Properties

        /// <summary>
        /// This property contains a reference to a repository.
        /// </summary>
        protected TRepository Repository { get; }

        #endregion

        // *******************************************************************
        // Constructors.
        // *******************************************************************

        #region Constructors

        /// <summary>
        /// This constructor creates a new instance of the <see cref="CrudStoreBase{TModel, TKey, TRepository}"/>
        /// class.
        /// </summary>
        /// <param name="repository">The repository to use with the store.</param>
        protected CrudStoreBase(
            TRepository repository
            )
        {
            // Validate the parameters before attempting to use them.
            Guard.Instance().ThrowIfNull(repository, nameof(repository));

            // Save the references.
            Repository = repository;
        }

        #endregion

        // *******************************************************************
        // Public methods.
        // *******************************************************************

        #region Public methods

        /// <summary>
        /// This method returns an <see cref="IQueryable{TModel}"/> object
        /// from the store..
        /// </summary>
        /// <returns>An <see cref="IQueryable{TModel}"/> object</returns>
        public virtual IQueryable<TModel> AsQueryable()
        {
            // Defer to the repository.
            return Repository.AsQueryable();
        }

        // *******************************************************************

        /// <summary>
        /// This method adds a new <typeparamref name="TModel"/> to the store.
        /// </summary>
        /// <param name="model">The model to use for the operation. </param>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>A task to perform the operation, that returns the recently
        /// added <typeparamref name="TModel"/> object.</returns>
        public virtual async Task<TModel> AddAsync(
            TModel model,
            CancellationToken cancellationToken = default
            )
        {
            // Validate the parameters before attempting to use them.
            Guard.Instance().ThrowIfNull(model, nameof(model));

            try
            {
                // Defer to the repository.
                var newModel = await Repository.AddAsync(
                    model,
                    cancellationToken
                    ).ConfigureAwait(false);

                // Return the result.
                return newModel;
            }
            catch (Exception ex)
            {
                // Add better context to the error.
                throw new StoreException(
                    message: string.Format(
                        Resources.CrudStoreBase_AddAsync,
                        GetType().Name,
                        typeof(TModel).Name,
                        JsonSerializer.Serialize(model)
                        ),
                    innerException: ex
                    );
            }
        }

        // *******************************************************************

        /// <summary>
        /// This method updates a <typeparamref name="TModel"/> in the store.
        /// </summary>
        /// <param name="model">The model to use for the operation. </param>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>A task to perform the operation, that returns the recently
        /// updated <typeparamref name="TModel"/> object.</returns>
        public virtual async Task<TModel> UpdateAsync(
            TModel model,
            CancellationToken cancellationToken = default
            )
        {
            // Validate the parameters before attempting to use them.
            Guard.Instance().ThrowIfNull(model, nameof(model));

            try
            {
                // Defer to the repository.
                var newModel = await Repository.UpdateAsync(
                    model,
                    cancellationToken
                    ).ConfigureAwait(false);

                // Return the result.
                return newModel;
            }
            catch (Exception ex)
            {
                // Add better context to the error.
                throw new StoreException(
                    message: string.Format(
                        Resources.CrudStoreBase_UpdateAsync,
                        GetType().Name,
                        typeof(TModel).Name,
                        JsonSerializer.Serialize(model)
                        ),
                    innerException: ex
                    );
            }
        }

        // *******************************************************************

        /// <summary>
        /// This method deletes a <typeparamref name="TModel"/> from the store.
        /// </summary>
        /// <param name="model">The model to use for the operation.</param>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>A task to perform the operation.</returns>
        public virtual async Task DeleteAsync(
            TModel model,
            CancellationToken cancellationToken = default
            )
        {
            // Validate the parameters before attempting to use them.
            Guard.Instance().ThrowIfNull(model, nameof(model));

            try
            {
                // Defer to the repository.
                await Repository.DeleteAsync(
                    model,
                    cancellationToken
                    ).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                // Add better context to the error.
                throw new StoreException(
                    message: string.Format(
                        Resources.CrudStoreBase_DeleteAsync,
                        GetType().Name,
                        typeof(TModel).Name,
                        JsonSerializer.Serialize(model)
                        ),
                    innerException: ex
                    );
            }
        }

        #endregion

        // *******************************************************************
        // Protected methods.
        // *******************************************************************

        #region Protected methods

        /// <summary>
        /// This method is called to clean up managed resources.
        /// </summary>
        /// <param name="disposing">True to cleanup managed resources.</param>
        protected override void Dispose(
            bool disposing
            )
        {
            // Should we cleanup managed resources?
            if (disposing)
            {
                (Repository as IDisposable)?.Dispose();
            }

            // Give the base class a chance.
            base.Dispose(disposing);
        }

        #endregion
    }



    /// <summary>
    /// This class represents a base implementation of the <see cref="ICrudStore{TModel, TKey}"/>
    /// interface.
    /// </summary>
    /// <typeparam name="TOptions">The type of associated options.</typeparam>
    /// <typeparam name="TModel">The model type associated with the repository.</typeparam>
    /// <typeparam name="TKey">The key type associated with the model.</typeparam>
    /// <typeparam name="TRepository">The type of associated repository.</typeparam>
    public abstract class CrudStoreBase<TOptions, TModel, TKey, TRepository> :
        CrudStoreBase<TModel, TKey, TRepository>,
        ICrudStore<TModel, TKey>
        where TModel : ModelBase<TKey>
        where TOptions : IOptions<StoreOptions>
        where TRepository : class, ICrudRepository<TModel, TKey>
        where TKey : new()
    {
        // *******************************************************************
        // Properties.
        // *******************************************************************

        #region Properties

        /// <summary>
        /// This property contains options for the store.
        /// </summary>
        protected TOptions Options { get; }

        #endregion

        // *******************************************************************
        // Constructors.
        // *******************************************************************

        #region Constructors

        /// <summary>
        /// This constructor creates a new instance of the <see cref="CrudStoreBase{TOptions, TModel, TKey, TRepository}"/>
        /// class.
        /// </summary>
        /// <param name="options">The options to use with the store.</param>
        /// <param name="repository">The repository to use with the store.</param>
        protected CrudStoreBase(
            TOptions options,
            TRepository repository
            ) : base(repository)
        {
            // Validate the parameters before attempting to use them.
            Guard.Instance().ThrowIfNull(options, nameof(options));

            // Save the referrence.
            Options = options;
        }

        #endregion
    }
}
