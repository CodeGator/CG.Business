using System;

namespace CG.Business.Models
{
    /// <summary>
    /// This class is a base implementation of the <see cref="IAuditedModel"/>
    /// interface.
    /// </summary>
    public abstract class AuditedModelBase : ModelBase, IAuditedModel
    {
        // *******************************************************************
        // Properties.
        // *******************************************************************

        #region Properties

        /// <inheritdoc />
        public DateTime CreatedDate { get; set; }

        /// <inheritdoc />
        public string CreatedBy { get; set; }

        /// <inheritdoc />
        public DateTime? UpdatedDate { get; set; }

        /// <inheritdoc />
        public string UpdatedBy { get; set; }

        #endregion
    }
}
