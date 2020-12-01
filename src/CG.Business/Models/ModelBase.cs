using CG.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
    /// <remarks>
    /// The idea, with this class, is to create a model base that contains a
    /// model key, which is a unique value that is irrespective of any other 
    /// identifiers that happen to be part of the associated database. Of course,
    /// this means storing an extra key. If that's an issue then don't use this
    /// version of <see cref="ModelBase{TKey}"/>
    /// </remarks>
    public class ModelBase<TKey> : ModelBase, IModel<TKey>
        where TKey : new()
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
}
