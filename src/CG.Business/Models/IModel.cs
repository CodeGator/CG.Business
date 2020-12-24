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
    public interface IModel<TKey> : IModel 
        where TKey : new()
    {
        /// <summary>
        /// This property contains the key for the model.
        /// </summary>
        TKey Key { get; set; }
    }



    /// <summary>
    /// This interface represents a business model.
    /// </summary>
    /// <typeparam name="TKey1">The type of associated model key 1.</typeparam>
    /// <typeparam name="TKey2">The type of associated model key 2.</typeparam>
    public interface IModel<TKey1, TKey2> : IModel
        where TKey1 : new()
        where TKey2 : new()
    {
        /// <summary>
        /// This property contains the key 1 for the model.
        /// </summary>
        TKey1 Key1 { get; set; }

        /// <summary>
        /// This property contains the key 2 for the model.
        /// </summary>
        TKey2 Key2 { get; set; }
    }



    /// <summary>
    /// This interface represents a business model.
    /// </summary>
    /// <typeparam name="TKey1">The type of associated model key 1.</typeparam>
    /// <typeparam name="TKey2">The type of associated model key 2.</typeparam>
    /// <typeparam name="TKey3">The type of associated model key 2.</typeparam>
    public interface IModel<TKey1, TKey2, TKey3> : IModel
        where TKey1 : new()
        where TKey2 : new()
        where TKey3 : new()
    {
        /// <summary>
        /// This property contains the key 1 for the model.
        /// </summary>
        TKey1 Key1 { get; set; }

        /// <summary>
        /// This property contains the key 2 for the model.
        /// </summary>
        TKey2 Key2 { get; set; }

        /// <summary>
        /// This property contains the key 3 for the model.
        /// </summary>
        TKey3 Key3 { get; set; }
    }
}
