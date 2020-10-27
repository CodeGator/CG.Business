using CG.Business.Repositories.Options;
using CG.Diagnostics;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CG.Business.Repositories
{
    /// <summary>
    /// This class is a test fixture for the <see cref="RepositoryBase{TOptions}"/> class.
    /// </summary>
    [TestClass]
    public class RepositoryBaseFixture
    {
        // *******************************************************************
        // Types.
        // *******************************************************************

        #region Types

        /// <summary>
        /// This class is used for internal testing purposes.
        /// </summary>
        class TestRepository : RepositoryBase<RepositoryOptions>
        {
            /// <summary>
            /// This constructor is used for internal testing purposes.
            /// </summary>
            public TestRepository(
                IOptions<RepositoryOptions> options
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
        /// This method ensures the <see cref="RepositoryBase{TraceOptions}.RepositoryBase(TraceOptions)"/> 
        /// constructor properly initializes object instances.
        /// </summary>
        [TestMethod]
        public void RepositoryBase_Ctor()
        {
            // Arrange ...
            var options = new OptionsWrapper<RepositoryOptions>(new RepositoryOptions()); 

            // Act ...
            var result = new TestRepository(options);

            // Assert ...
            Assert.IsTrue(
                result.GetPropertyValue("Options", true) != null,
                "The Options property wasn't initialized by the ctor."
                );
        }

        #endregion
    }
}
