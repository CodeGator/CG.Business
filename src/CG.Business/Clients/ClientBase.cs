using CG.Business.Clients.Options;
using CG.Validations;
using Microsoft.Extensions.Options;
using System;

namespace CG.Business.Clients
{
    /// <summary>
    /// This class is a base implementation of the <see cref="IClient"/>
    /// interface.
    /// </summary>
    public abstract class ClientBase : DisposableBase, IClient
    {

    }


    /// <summary>
    /// This class represents a base implementation of the <see cref="IClient"/>
    /// interface.
    /// </summary>
    /// <typeparam name="TOptions">The type of associated options.</typeparam>
    public abstract class ClientBase<TOptions> :
        ClientBase,
        IClient
        where TOptions : ClientOptions, new()
    {
        // *******************************************************************
        // Properties.
        // *******************************************************************

        #region Properties

        /// <summary>
        /// This property contains options for the director.
        /// </summary>
        protected IOptions<TOptions> Options { get; }

        #endregion

        // *******************************************************************
        // Constructors.
        // *******************************************************************

        #region Constructors

        /// <summary>
        /// This constructor creates a new instance of the <see cref="ClientBase{TOptions}"/>
        /// class.
        /// </summary>
        /// <param name="options">The options to use with the director.</param>
        public ClientBase(
            IOptions<TOptions> options
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
