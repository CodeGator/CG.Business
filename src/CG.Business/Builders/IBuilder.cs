using Microsoft.Extensions.DependencyInjection;
using System;

namespace CG.Business.Builders
{
    /// <summary>
    /// This interface represents an object that builds up other 
    /// object types, at runtime.
    /// </summary>
    public interface IBuilder
    {
        /// <summary>
        /// This property contains a reference to a service collection.
        /// </summary>
        IServiceCollection ServiceCollection { get; set; }
    }
}
