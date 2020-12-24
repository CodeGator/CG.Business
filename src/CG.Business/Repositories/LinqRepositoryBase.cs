using CG.Business.Models;
using CG.Business.Repositories;
using CG.Business.Repositories.Options;
using Microsoft.Extensions.Options;
using System;
using System.Linq;

namespace CG.Business.Repositories
{
    /// <summary>
    /// This class represents a base implementation of the <see cref="IRepository"/>
    /// interface.
    /// </summary>
    /// <typeparam name="TOptions">The type of associated options.</typeparam>
    /// <typeparam name="TModel">The type of associated model.</typeparam>
    public abstract class LinqRepositoryBase<TOptions, TModel> :
        RepositoryBase<TOptions>,
        ILinqRepository<TModel>
        where TModel : class, IModel
        where TOptions : IOptions<RepositoryOptions>
    {
        // *******************************************************************
        // Constructors.
        // *******************************************************************

        #region Constructors

        /// <summary>
        /// This constructor creates a new instance of the <see cref="LinqRepositoryBase{TOptions, TModel}"/>
        /// class.
        /// </summary>
        /// <param name="options">The options to use with the repository.</param>
        protected LinqRepositoryBase(
            TOptions options
            ) : base(options)
        {
            
        }

        #endregion

        // *******************************************************************
        // Public methods.
        // *******************************************************************

        #region Public methods

        /// <summary>
        /// This method returns an <see cref="IQueryable{TModel}"/> object.
        /// </summary>
        /// <returns>An <see cref="IQueryable{TModel}"/> object</returns>
        public abstract IQueryable<TModel> AsQueryable();

        #endregion
    }


    /// <summary>
    /// This class represents a base implementation of the <see cref="IRepository"/>
    /// interface that adds an additional type parameter: <typeparamref name="TKey"/>.
    /// </summary>
    /// <typeparam name="TOptions">The type of associated options.</typeparam>
    /// <typeparam name="TModel">The type of associated model.</typeparam>
    /// <typeparam name="TKey">The key type associated with the model.</typeparam>
    public abstract class LinqRepositoryBase<TOptions, TModel, TKey> :
        LinqRepositoryBase<TOptions, TModel>,
        ILinqRepository<TModel>
        where TModel : class, IModel<TKey>
        where TOptions : IOptions<RepositoryOptions>
        where TKey : new()
    {
        // *******************************************************************
        // Constructors.
        // *******************************************************************

        #region Constructors

        /// <summary>
        /// This constructor creates a new instance of the <see cref="LinqRepositoryBase{TOptions, TModel, TKey}"/>
        /// class.
        /// </summary>
        /// <param name="options">The options to use with the repository.</param>
        protected LinqRepositoryBase(
            TOptions options
            ) : base(options)
        {

        }

        #endregion
    }



    /// <summary>
    /// This class represents a base implementation of the <see cref="IRepository"/>
    /// interface that adds additional type parameters: <typeparamref name="TKey1"/>
    /// and <typeparamref name="TKey2"/>.
    /// </summary>
    /// <typeparam name="TOptions">The type of associated options.</typeparam>
    /// <typeparam name="TModel">The type of associated model.</typeparam>
    /// <typeparam name="TKey1">The key 1 type associated with the model.</typeparam>
    /// <typeparam name="TKey2">The key 2 type associated with the model.</typeparam>
    public abstract class LinqRepositoryBase<TOptions, TModel, TKey1, TKey2> :
        LinqRepositoryBase<TOptions, TModel>,
        ILinqRepository<TModel>
        where TModel : class, IModel<TKey1, TKey2>
        where TOptions : IOptions<RepositoryOptions>
        where TKey1 : new()
        where TKey2 : new()
    {
        // *******************************************************************
        // Constructors.
        // *******************************************************************

        #region Constructors

        /// <summary>
        /// This constructor creates a new instance of the <see cref="LinqRepositoryBase{TOptions, TModel, TKey1, TKey2}"/>
        /// class.
        /// </summary>
        /// <param name="options">The options to use with the repository.</param>
        protected LinqRepositoryBase(
            TOptions options
            ) : base(options)
        {

        }

        #endregion
    }



    /// <summary>
    /// This class represents a base implementation of the <see cref="IRepository"/>
    /// interface that adds additional type parameters: <typeparamref name="TKey1"/>,
    /// <typeparamref name="TKey2"/> and <typeparamref name="TKey3"/>.
    /// </summary>
    /// <typeparam name="TOptions">The type of associated options.</typeparam>
    /// <typeparam name="TModel">The type of associated model.</typeparam>
    /// <typeparam name="TKey1">The key 1 type associated with the model.</typeparam>
    /// <typeparam name="TKey2">The key 2 type associated with the model.</typeparam>
    /// <typeparam name="TKey3">The key 3 type associated with the model.</typeparam>
    public abstract class LinqRepositoryBase<TOptions, TModel, TKey1, TKey2, TKey3> :
        LinqRepositoryBase<TOptions, TModel>,
        ILinqRepository<TModel>
        where TModel : class, IModel<TKey1, TKey2, TKey3>
        where TOptions : IOptions<RepositoryOptions>
        where TKey1 : new()
        where TKey2 : new()
        where TKey3 : new()
    {
        // *******************************************************************
        // Constructors.
        // *******************************************************************

        #region Constructors

        /// <summary>
        /// This constructor creates a new instance of the <see cref="LinqRepositoryBase{TOptions, TModel, TKey1, TKey2, TKey3}"/>
        /// class.
        /// </summary>
        /// <param name="options">The options to use with the repository.</param>
        protected LinqRepositoryBase(
            TOptions options
            ) : base(options)
        {

        }

        #endregion
    }
}
