using CG.Business.Repositories.Options;
using CG.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CG.Business.Models
{
    /// <summary>
    /// This class is a test fixture for the <see cref="ModelBase{TKey}"/> class.
    /// </summary>
    [TestClass]
    public class ModelBaseFixture
    {
        // *******************************************************************
        // Types.
        // *******************************************************************

        #region Types

        /// <summary>
        /// This class is used for internal testing purposes.
        /// </summary>
        class TestModel : ModelBase<int>
        {

        }

        #endregion

        // *******************************************************************
        // Public methods.
        // *******************************************************************

        #region Public methods

        /// <summary>
        /// This method ensures the <see cref="ModelBase{TraceOptions}.GetHashCode"/> 
        /// method returns a hashcode for the identifier.
        /// </summary>
        [TestMethod]
        public void ModelBase_GetHashCode()
        {
            // Arrange ...
            
            // Act ...
            var result = new TestModel()
            {
                Id = 400
            };

            // Assert ...
            Assert.IsTrue(
                result.GetHashCode() == 400.GetHashCode(),
                "The hashcode was invalid."
                );
        }

        // *******************************************************************

        /// <summary>
        /// This method ensures the <see cref="ModelBase{TraceOptions}.Equals(object)"/> 
        /// method returns true of the objects are equal.
        /// </summary>
        [TestMethod]
        public void ModelBase_Equals()
        {
            // Arrange ...
            var a = new TestModel()
            {
                Id = 400
            };
            var b = new TestModel()
            {
                Id = 400
            };

            // Act ...
            var result = a.Equals(b);

            // Assert ...
            Assert.IsTrue(
                result,
                "The comparison returned an invalid result."
                );
        }

        // *******************************************************************

        /// <summary>
        /// This method ensures the <see cref="ModelBase{TraceOptions}.ToString()"/> 
        /// method returns a valid string representation of the object.
        /// </summary>
        [TestMethod]
        public void ModelBase_ToString()
        {
            // Arrange ...
            var a = new TestModel()
            {
                Id = 400
            };

            // Act ...
            var result = a.ToString();

            // Assert ...
            Assert.IsTrue(
                result == "CG.Business.Models.ModelBaseFixture+TestModel - Id: 400",
                "The method returned an invalid result."
                );
        }

        #endregion
    }
}
