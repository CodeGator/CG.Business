using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using CG.Business.Repositories.Options;

namespace CG.Business.Repositories
{
    /// <summary>
    /// This class is a test fixture for the <see cref="ServiceCollectionExtensions"/>
    /// class.
    /// </summary>
    [TestClass]
    public class ServiceCollectionExtensionsFixture
    {
        // *******************************************************************
        // Properties.
        // *******************************************************************

        #region Properties

        /// <summary>
        /// This property contains a configuration with well known test values.
        /// </summary>
        protected IConfiguration Configuration { get; set; }

        #endregion

        // *******************************************************************
        // Public methods.
        // *******************************************************************

        #region Public methods

        /// <summary>
        /// This method is called to initialize the fixture before a test run.
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            var builder = new ConfigurationBuilder();
            builder.AddInMemoryCollection(
                new KeyValuePair<string, string>[]
                {
                    new KeyValuePair<string, string>($"CG.TestMe", ""),
                    new KeyValuePair<string, string>($"CG.TestMe:{RepositoryOptions.SectionName}", ""),
                    new KeyValuePair<string, string>($"CG.TestMe:{RepositoryOptions.SectionName}:Strategy", "TestMe"),
                    new KeyValuePair<string, string>($"CG.TestMe:{RepositoryOptions.SectionName}:TestMe", ""),
                    new KeyValuePair<string, string>($"CG.TestMe:{RepositoryOptions.SectionName}:TestMe:ConnectionString", "testing"),
                });
            Configuration = builder.Build();
        }

        // *******************************************************************

        /// <summary>
        /// This method ensures the <see cref="ServiceCollectionExtensions.AddRepositories(IServiceCollection, IConfiguration, string, string)"/>
        /// method correctly loads and calls a configured extension method.
        /// </summary>
        [TestMethod]
        public void ServiceCollectionExtensions_AddRepositories()
        {
            // Arrange ...
            var serviceCollection = new ServiceCollection();
            var section = Configuration.GetSection($"CG.TestMe");

            // Act ...
            serviceCollection.AddRepositories(section);

            // Assert ...
            Assert.IsTrue(
                X.MethodCalled,
                "The extension method wasn't called."
                );
            Assert.IsTrue(
                X.ConfigOk,
                "The configuration was invalid in the extension method."
                );
        }

        #endregion
    }

    /// <summary>
    /// This class contains dummy test methods.
    /// </summary>
    public static class X
    {
        // *******************************************************************
        // Properties.
        // *******************************************************************

        #region Properties

        /// <summary>
        /// This method indicates the extension method was called.
        /// </summary>
        public static bool MethodCalled { get; set; } = false;

        /// <summary>
        /// This method indicates the proper configuration section was passed 
        /// to the extension method.
        /// </summary>
        public static bool ConfigOk { get; set; } = false;

        #endregion

        // *******************************************************************
        // Public methods.
        // *******************************************************************

        #region Public methods

        public static IServiceCollection AddTestMe(
            this IServiceCollection serviceCollection,
            IConfiguration confuguration
            )
        {
            MethodCalled = true;
            ConfigOk = confuguration["ConnectionString"] == "testing";
            return serviceCollection;
        }

        #endregion
    }
}
