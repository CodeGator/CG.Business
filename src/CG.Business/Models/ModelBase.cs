using CG.DataAnnotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CG.Models
{
    /// <summary>
    /// This class represents a base implmentation of a business model. 
    /// </summary>
    public class ModelBase : ValidatableObject
    {

    }


    /// <summary>
    /// This class represents a base implmentation of a business model.
    /// </summary>
    /// <typeparam name="T">The type of associated unique identifier.</typeparam>
    public class ModelBase<T> : ModelBase
    {
        // *******************************************************************
        // Properties.
        // *******************************************************************

        #region Properties

        /// <summary>
        /// This property contains the unique identifier for the model.
        /// </summary>
        [Key]
        public T Id { get; set; }

        #endregion

        // *******************************************************************
        // Public methods.
        // *******************************************************************

        #region Public methods

        /// <summary>
        /// This method is overridden in order to generate a hash code for 
        /// the model.
        /// </summary>
        /// <returns>An integer hash code that represents the model.</returns>
        public override int GetHashCode()
        {
            // Return a hash code for the id.
            return Id.GetHashCode();
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
            return EqualityComparer<T>.Default.Equals(
                Id, 
                (obj as ModelBase<T>).Id
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
            return $"{base.ToString()} - Id: {Id}";
        }

        #endregion
    }
}
