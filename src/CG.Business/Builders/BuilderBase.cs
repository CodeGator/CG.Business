using Microsoft.Extensions.DependencyInjection;
using System;

namespace CG.Business.Builders
{
    /// <summary>
    /// This class is a default implementation of the <see cref="IBuilder"/>
    /// interface.
    /// </summary>
    public abstract class BuilderBase : IBuilder
    {
        // *******************************************************************
        // Properties.
        // *******************************************************************

        #region Properties

        /// <summary>
        /// This property contains a reference to a service collection.
        /// </summary>
        public IServiceCollection ServiceCollection { get; set; }

        #endregion
    }
}
