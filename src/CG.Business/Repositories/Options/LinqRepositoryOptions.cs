using CG.Business.Properties;
using System;
using System.ComponentModel.DataAnnotations;

namespace CG.Business.Repositories.Options
{
    /// <summary>
    /// This class represents configuration options for a LINQ based
    /// repository.
    /// </summary>
    public class LinqRepositoryOptions : RepositoryOptions
    {
        // *******************************************************************
        // Properties.
        // *******************************************************************

        #region Properties

        /// <summary>
        /// This property contains the connection string for the LINQ connection.
        /// </summary>
        [Required(ErrorMessageResourceName = "LinqRepositoryOptions_CS",
                  ErrorMessageResourceType = typeof(Resources))]
        public string ConnectionString { get; set; }

        #endregion
    }
}
