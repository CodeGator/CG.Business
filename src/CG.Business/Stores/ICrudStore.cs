using CG.Business.Models;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CG.Business.Stores
{
    /// <summary>
    /// This interface represents a store type that includes basic CRUD 
    /// operations.
    /// </summary>
    public interface ICrudStore<TModel, TKey> : IStore
        where TModel : ModelBase<TKey>
    {
        /// <summary>
        /// This method returns an <see cref="IQueryable{TModel}"/> object
        /// from the store.
        /// </summary>
        /// <returns>An <see cref="IQueryable{TModel}"/> object</returns>
        IQueryable<TModel> AsQueryable();

        /// <summary>
        /// This method adds a new <typeparamref name="TModel"/> to the store.
        /// </summary>
        /// <param name="model">The model to use for the operation. </param>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>A task to perform the operation, that returns the recently
        /// added <typeparamref name="TModel"/> object.</returns>
        Task<TModel> AddAsync(
            TModel model,
            CancellationToken cancellationToken = default
            );

        /// <summary>
        /// This method updates a <typeparamref name="TModel"/> in the store.
        /// </summary>
        /// <param name="model">The model to use for the operation. </param>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>A task to perform the operation, that returns the recently
        /// updated <typeparamref name="TModel"/> object.</returns>
        Task<TModel> UpdateAsync(
            TModel model,
            CancellationToken cancellationToken = default
            );

        /// <summary>
        /// This method deletes a <typeparamref name="TModel"/> from the store.
        /// </summary>
        /// <param name="model">The model to use for the operation.</param>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>A task to perform the operation.</returns>
        Task DeleteAsync(
            TModel model,
            CancellationToken cancellationToken = default
            );

    }
}
