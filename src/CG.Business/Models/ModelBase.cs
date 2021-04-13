using CG.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CG.Business.Models
{
    /// <summary>
    /// This class is a default implmentation of the <see cref="IModel"/>
    /// interface.
    /// </summary>
    public class ModelBase : ValidatableObject, IModel
    {

    }

    /// <summary>
    /// This class is a default implmentation of the <see cref="IModel"/>
    /// interface.
    /// </summary>
    /// <typeparam name="TKey">The type of associated model key.</typeparam>
    public class ModelBase<TKey> : ModelBase, IModel<TKey>
    {
        // *******************************************************************
        // Properties.
        // *******************************************************************

        #region Properties

        /// <inheritdoc />
        [Key]
        public TKey Key { get; set; }

        #endregion

        // *******************************************************************
        // Public methods.
        // *******************************************************************

        #region Public methods

        /// <summary>
        /// This method is overridden in order to generate a unique hash code 
        /// for the model.
        /// </summary>
        /// <returns>An integer hash code that represents the model.</returns>
        public override int GetHashCode()
        {
            // Return a hash code for the key.
            return Key.GetHashCode();
        }

        // *******************************************************************

        /// <summary>
        /// This method is overriden in order to determine equality.
        /// </summary>
        /// <param name="obj">The model to compare with.</param>
        /// <returns>True if the objects are equal; false otherwise.</returns>
        public override bool Equals(object obj)
        {
            // If the parameter is null, can't be equal.
            if (null == obj)
            {
                return false;
            }

            // If the types don't match, can't be equal.
            if (GetType() != obj.GetType())
            {
                return false;
            }

            // Return an equality comparison of the id properties.
            return EqualityComparer<TKey>.Default.Equals(
                Key,
                (obj as ModelBase<TKey>).Key
                );
        }

        // *******************************************************************

        /// <summary>
        /// This method returns a string that represents the current model.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            // Return a string representation of the object.
            return $"{base.ToString()} - Key: {Key}";
        }

        #endregion
    }


    /// <summary>
    /// This class is a default implementation of the <see cref="IModel{TKey1, TKey2}"/>
    /// interface.
    /// </summary>
    /// <typeparam name="TKey1">The type of associated model key 1.</typeparam>
    /// <typeparam name="TKey2">The type of associated model key 2.</typeparam>
    public class ModelBase<TKey1, TKey2> : ModelBase, IModel<TKey1, TKey2>
    {
        // *******************************************************************
        // Properties.
        // *******************************************************************

        #region Properties

        /// <inheritdoc />
        [Key, Column(Order = 0)]
        public TKey1 Key1 { get; set; }

        /// <inheritdoc />
        [Key, Column(Order = 1)]
        public TKey2 Key2 { get; set; }

        #endregion

        // *******************************************************************
        // Public methods.
        // *******************************************************************

        #region Public methods

        /// <summary>
        /// This method is overridden in order to generate a unique hash code 
        /// for the model.
        /// </summary>
        /// <returns>An integer hash code that represents the model.</returns>
        public override int GetHashCode()
        {
            // Return a hash code for the key.
            return Key1.GetHashCode() + Key2.GetHashCode();
        }

        // *******************************************************************

        /// <summary>
        /// This method is overriden in order to determine equality.
        /// </summary>
        /// <param name="obj">The model to compare with.</param>
        /// <returns>True if the objects are equal; false otherwise.</returns>
        public override bool Equals(object obj)
        {
            // If the parameter is null, can't be equal.
            if (null == obj)
            {
                return false;
            }

            // If the types don't match, can't be equal.
            if (GetType() != obj.GetType())
            {
                return false;
            }

            // Return an equality comparison of the key properties.
            return EqualityComparer<TKey1>.Default.Equals(
                Key1,
                (obj as ModelBase<TKey1, TKey2>).Key1
                ) &&
                EqualityComparer<TKey2>.Default.Equals(
                Key2,
                (obj as ModelBase<TKey1, TKey2>).Key2
                );
        }

        // *******************************************************************

        /// <summary>
        /// This method returns a string that represents the current model.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            // Return a string representation of the object.
            return $"{base.ToString()} - Key1: {Key1}, Key2: {Key2}";
        }

        #endregion
    }



    /// <summary>
    /// This class is a default implementation of the <see cref="IModel{TKey1, TKey2, TKey3}"/>
    /// interface.
    /// </summary>
    /// <typeparam name="TKey1">The type of associated model key 1.</typeparam>
    /// <typeparam name="TKey2">The type of associated model key 2.</typeparam>
    /// <typeparam name="TKey3">The type of associated model key 3.</typeparam>
    public class ModelBase<TKey1, TKey2, TKey3> : ModelBase, IModel<TKey1, TKey2, TKey3>
    {
        // *******************************************************************
        // Properties.
        // *******************************************************************

        #region Properties

        /// <inheritdoc />
        [Key, Column(Order = 0)]
        public TKey1 Key1 { get; set; }

        /// <inheritdoc />
        [Key, Column(Order = 1)]
        public TKey2 Key2 { get; set; }

        /// <inheritdoc />
        [Key, Column(Order = 2)]
        public TKey3 Key3 { get; set; }

        #endregion

        // *******************************************************************
        // Public methods.
        // *******************************************************************

        #region Public methods

        /// <summary>
        /// This method is overridden in order to generate a unique hash code 
        /// for the model.
        /// </summary>
        /// <returns>An integer hash code that represents the model.</returns>
        public override int GetHashCode()
        {
            // Return a hash code for the key.
            return Key1.GetHashCode() + Key2.GetHashCode() + Key3.GetHashCode();
        }

        // *******************************************************************

        /// <summary>
        /// This method is overriden in order to determine equality.
        /// </summary>
        /// <param name="obj">The model to compare with.</param>
        /// <returns>True if the objects are equal; false otherwise.</returns>
        public override bool Equals(object obj)
        {
            // If the parameter is null, can't be equal.
            if (null == obj)
            {
                return false;
            }

            // If the types don't match, can't be equal.
            if (GetType() != obj.GetType())
            {
                return false;
            }

            // Return an equality comparison of the key properties.
            return EqualityComparer<TKey1>.Default.Equals(
                Key1,
                (obj as ModelBase<TKey1, TKey2, TKey3>).Key1
                ) &&
                EqualityComparer<TKey2>.Default.Equals(
                Key2,
                (obj as ModelBase<TKey1, TKey2, TKey3>).Key2
                ) &&
                EqualityComparer<TKey3>.Default.Equals(
                Key3,
                (obj as ModelBase<TKey1, TKey2, TKey3>).Key3
                );
        }

        // *******************************************************************

        /// <summary>
        /// This method returns a string that represents the current model.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            // Return a string representation of the object.
            return $"{base.ToString()} - Key1: {Key1}, Key2: {Key2}, Key3: {Key3}";
        }

        #endregion
    }
}
