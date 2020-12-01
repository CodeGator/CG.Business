using System;

namespace CG.Business.Models
{
    /// <summary>
    /// This interface represents a business model.
    /// </summary>
    public interface IModel
    {

    }


    /// <summary>
    /// This interface represents a business model.
    /// </summary>
    /// <typeparam name="TKey">The type of associated model key.</typeparam>
    /// <remarks>
    /// The idea, with this flavor of <see cref="IModel"/>, is to create a type
    /// that contains a unique key, which is irrespective of any other keys or 
    /// identifiers that happen to be part of the underlying database. Of course,
    /// this almost certainly means storing an extra key. If that's an issue then 
    /// don't use this flavor of <see cref="IModel"/>
    /// </remarks>
    public interface IModel<TKey> : IModel 
        where TKey : new()
    {
        /// <summary>
        /// This property contains the key for the model.
        /// </summary>
        TKey Key { get; set; }
    }
}
