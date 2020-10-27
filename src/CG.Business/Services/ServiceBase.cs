using CG.Business.Models;
using CG.Business.Services.Options;
using CG.Validations;
using Microsoft.Extensions.Options;

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
    /// interface.
    /// </summary>
    /// <typeparam name="TOptions">The type of associated options.</typeparam>
    public abstract class ServiceBase<TOptions> :
        ServiceBase,
        IService
        where TOptions : IOptions<ServiceOptions>
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
}
