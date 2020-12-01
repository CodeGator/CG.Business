using System.Linq;

namespace CG.Business.Repositories
{
    /// <summary>
    /// This interface represents a repository type that exposes an <see cref="IQueryable{T}"/>. 
    /// object, for queries, but has no built-in mechanism for writing to the underyling
    /// data store.
    /// </summary>
    /// <typeparam name="TModel">The model type associated with the repository.</typeparam>
    public interface ILinqRepository<TModel> : IRepository
    {
        /// <summary>
        /// This method returns an <see cref="IQueryable{TModel}"/> object
        /// from the repository.
        /// </summary>
        /// <returns>An <see cref="IQueryable{TModel}"/> object</returns>
        IQueryable<TModel> AsQueryable();
    }
}
