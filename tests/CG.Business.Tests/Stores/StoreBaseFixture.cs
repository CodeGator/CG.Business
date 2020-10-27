using CG.Business.Stores.Options;
using CG.Diagnostics;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CG.Business.Stores
{
    /// <summary>
    /// This class is a test fixture for the <see cref="StoreBase{TOptions}"/> class.
    /// </summary>
    [TestClass]
    public class StoreBaseFixture
    {
        // *******************************************************************
        // Types.
        // *******************************************************************

        #region Types

        /// <summary>
        /// This class is used for internal testing purposes.
        /// </summary>
        class TestStore : StoreBase<StoreOptions>
        {
            /// <summary>
            /// This constructor is used for internal testing purposes.
            /// </summary>
            public TestStore(
                IOptions<StoreOptions> options
                ) : base(options)
            {

            }
        }

        #endregion

        // *******************************************************************
        // Public methods.
        // *******************************************************************

        #region Public methods

        /// <summary>
        /// This method ensures the <see cref="StoreBase{TraceOptions}.StoreBase(TraceOptions)"/> 
        /// constructor properly initializes object instances.
        /// </summary>
        [TestMethod]
        public void StoreBase_Ctor()
        {
            // Arrange ...
            var options = new OptionsWrapper<StoreOptions>(new StoreOptions());

            // Act ...
            var result = new TestStore(options);

            // Assert ...
            Assert.IsTrue(
                result.GetPropertyValue("Options", true) != null,
                "The Options property wasn't initialized by the ctor."
                );
        }

        #endregion
    }
}
