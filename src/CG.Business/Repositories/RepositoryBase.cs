using CG.Business.Repositories.Options;
using CG.Models;
using CG.Validations;

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
    /// inteface.
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
    /// inteface.
    /// </summary>
    /// <typeparam name="TSettings">The type of associated settings.</typeparam>
    public abstract class RepositoryBase<TSettings> :
        RepositoryBase,
        IRepository
        where TSettings : RepositoryOptions
    {
        // *******************************************************************
        // Properties.
        // *******************************************************************

        #region Properties

        /// <summary>
        /// This property contains settings for the manager.
        /// </summary>
        protected TSettings Settings { get; }

        #endregion

        // *******************************************************************
        // Constructors.
        // *******************************************************************

        #region Constructors

        /// <summary>
        /// This constructor creates a new instance of the <see cref="RepositoryBase{TSettings}"/>
        /// class.
        /// </summary>
        /// <param name="settings">The settings to use with the repository.</param>
        public RepositoryBase(
            TSettings settings
            )
        {
            // Validate the parameters before attempting to use them.
            Guard.Instance().ThrowIfNull(settings, nameof(settings));

            // Save the referrence.
            Settings = settings;
        }

        #endregion
    }


    /// <summary>
    /// This class represents a base implementation of the <see cref="IRepository"/>
    /// inteface.
    /// </summary>
    /// <typeparam name="TSettings">The type of associated settings.</typeparam>
    /// <typeparam name="TEntity">The entity type associated with the repository.</typeparam>
    /// <typeparam name="TModel">The model type associated with the repository.</typeparam>
    public abstract class RepositoryBase<TSettings, TEntity, TModel> :
        RepositoryBase<TEntity, TModel>,
        IRepository
        where TSettings : RepositoryOptions
        where TEntity : class
        where TModel : ModelBase
    {
        // *******************************************************************
        // Properties.
        // *******************************************************************

        #region Properties

        /// <summary>
        /// This property contains settings for the manager.
        /// </summary>
        protected TSettings Settings { get; }

        #endregion

        // *******************************************************************
        // Constructors.
        // *******************************************************************

        #region Constructors

        /// <summary>
        /// This constructor creates a new instance of the <see cref="RepositoryBase{TSettings}"/>
        /// class.
        /// </summary>
        /// <param name="settings">The settings to use with the repository.</param>
        public RepositoryBase(
            TSettings settings
            )
        {
            // Validate the parameters before attempting to use them.
            Guard.Instance().ThrowIfNull(settings, nameof(settings));

            // Save the referrence.
            Settings = settings;
        }

        #endregion
    }
}
