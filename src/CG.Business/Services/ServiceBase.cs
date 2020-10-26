using CG.Business.Models;
using CG.Business.Services.Options;
using CG.Validations;

namespace CG.Business.Services
{
    /// <summary>
    /// This class represents a base implementation of the <see cref="IService"/>
    /// inteface.
    /// </summary>
    public abstract class ServiceBase :
        DisposableBase,
        IService
    {

    }


    /// <summary>
    /// This class represents a base implementation of the <see cref="IService"/>
    /// interace.
    /// </summary>
    /// <typeparam name="TEntity">The entity type associated with the service.</typeparam>
    /// <typeparam name="TModel">The model type associated with the service.</typeparam>
    public abstract class ServiceBase<TEntity, TModel> :
        ServiceBase,
        IService
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
    /// This class represents a base implementation of the <see cref="IService"/>
    /// interface.
    /// </summary>
    /// <typeparam name="TOptions">The type of associated options.</typeparam>
    public abstract class ServiceBase<TOptions> :
        ServiceBase,
        IService
        where TOptions : ServiceOptions
    {
        // *******************************************************************
        // Properties.
        // *******************************************************************

        #region Properties

        /// <summary>
        /// This property contains options for the service.
        /// </summary>
        protected TOptions Options { get; }

        #endregion

        // *******************************************************************
        // Constructors.
        // *******************************************************************

        #region Constructors

        /// <summary>
        /// This constructor creates a new instance of the <see cref="ServiceBase{TOptions}"/>
        /// class.
        /// </summary>
        /// <param name="options">The options to use with the service.</param>
        public ServiceBase(
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
    /// This class represents a base implementation of the <see cref="IService"/>
    /// interface.
    /// </summary>
    /// <typeparam name="TOptions">The type of associated options.</typeparam>
    /// <typeparam name="TEntity">The entity type associated with the service.</typeparam>
    /// <typeparam name="TModel">The model type associated with the service.</typeparam>
    public abstract class ServiceBase<TOptions, TEntity, TModel> :
        ServiceBase<TEntity, TModel>,
        IService
        where TOptions : ServiceOptions
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
        /// This constructor creates a new instance of the <see cref="ServiceBase{TOptions}"/>
        /// class.
        /// </summary>
        /// <param name="options">The options to use with the service.</param>
        public ServiceBase(
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
