using CG.Business.Models;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CG.Business.Repositories
{
    /// <summary>
    /// This interface represents a repository type that includes simple
    /// CRUD operations and queries through the use of the LINQ <see cref="IQueryable{T}"/>. 
    /// type.
    /// </summary>
    public interface ICrudRepository<TModel, TKey> : IRepository
        where TModel : ModelBase<TKey>
    {
        /// <summary>
        /// This method returns an <see cref="IQueryable{TModel}"/> object
        /// from the repository.
        /// </summary>
        /// <returns>An <see cref="IQueryable{TModel}"/> object</returns>
        /// <remarks>
        /// <para>
        /// The idea, with this method, is to expose the internals of the 
        /// underlying data store directly to the caller. I realize that means
        /// the abstraction is leaking implementation details. But, because of
        /// the way LINQ works, it's also the simplest way of doing business 
        /// without forcing everyone who uses this type to jump through hoops 
        /// trying to simultaneously hide, and expose, those very same details. 
        /// So yes, this abstraction is leakier than a $2 rowboat in a hurricane, 
        /// but, it's also usable without forcing layers of additional code onto 
        /// everyone that, ultimately, will probably also leak in ways we wish it 
        /// wouldn't. LINQ is a flexible tool we all love to use, but, it's also 
        /// the culprit here. If we use LINQ for queries (as I'm doing her), then 
        /// we can live with the benefits of that decision. Of course, it also 
        /// means we have to live with the consequences of that decision ... 
        /// </para>
        /// <para>
        /// An alternative to this type of repository is the <see cref="RepositoryBase"/>
        /// type, and it's variants. They all lend themselves quite nicely to 
        /// the use of ADO for creating highly performant query methods that 
        /// don't leak implementation details. 
        /// </para>
        /// </remarks>
        IQueryable<TModel> AsQueryable();

        /// <summary>
        /// This method adds a new <typeparamref name="TModel"/> to the 
        /// repository.
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
        /// This method updates a <typeparamref name="TModel"/> in the 
        /// repository.
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
        /// This method deletes a <typeparamref name="TModel"/> from the 
        /// repository.
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
