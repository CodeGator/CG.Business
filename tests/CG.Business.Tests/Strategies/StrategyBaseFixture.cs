using CG.Business.Strategies.Options;
using CG.Diagnostics;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CG.Business.Strategies
{
    /// <summary>
    /// This class is a test fixture for the <see cref="StrategyBase{TOptions}"/> class.
    /// </summary>
    [TestClass]
    public class StrategyBaseFixture
    {
        // *******************************************************************
        // Types.
        // *******************************************************************

        #region Types

        /// <summary>
        /// This class is used for internal testing purposes.
        /// </summary>
        class TestStrategy : StrategyBase<StrategyOptions>
        {
            /// <summary>
            /// This constructor is used for internal testing purposes.
            /// </summary>
            public TestStrategy(
                IOptions<StrategyOptions> options
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
        /// This method ensures the <see cref="StrategyBase{TraceOptions}.StrategyBase(TraceOptions)"/> 
        /// constructor properly initializes object instances.
        /// </summary>
        [TestMethod]
        public void StrategyBase_Ctor()
        {
            // Arrange ...
            var options = new OptionsWrapper<StrategyOptions>(new StrategyOptions());

            // Act ...
            var result = new TestStrategy(options);

            // Assert ...
            Assert.IsTrue(
                result.GetPropertyValue("Options", true) != null,
                "The Options property wasn't initialized by the ctor."
                );
        }

        #endregion
    }
}
