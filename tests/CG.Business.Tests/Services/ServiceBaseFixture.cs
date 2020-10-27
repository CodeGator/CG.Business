using CG.Business.Services.Options;
using CG.Diagnostics;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CG.Business.Services
{
    /// <summary>
    /// This class is a test fixture for the <see cref="ServiceBase{TOptions}"/> class.
    /// </summary>
    [TestClass]
    public class ServiceBaseFixture
    {
        // *******************************************************************
        // Types.
        // *******************************************************************

        #region Types

        /// <summary>
        /// This class is used for internal testing purposes.
        /// </summary>
        class TestService : ServiceBase<ServiceOptions>
        {
            /// <summary>
            /// This constructor is used for internal testing purposes.
            /// </summary>
            public TestService(
                IOptions<ServiceOptions> options
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
        /// This method ensures the <see cref="ServiceBase{TraceOptions}.ServiceBase(TraceOptions)"/> 
        /// constructor properly initializes object instances.
        /// </summary>
        [TestMethod]
        public void ServiceBase_Ctor()
        {
            // Arrange ...
            var options = new OptionsWrapper<ServiceOptions>(new ServiceOptions());

            // Act ...
            var result = new TestService(options);

            // Assert ...
            Assert.IsTrue(
                result.GetPropertyValue("Options", true) != null,
                "The Options property wasn't initialized by the ctor."
                );
        }

        #endregion
    }
}
