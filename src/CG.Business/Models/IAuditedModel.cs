using System;
using System.ComponentModel.DataAnnotations;

namespace CG.Business.Models
{
    /// <summary>
    /// This interface represents a business model with audit information.
    /// </summary>
    public interface IAuditedModel : IModel
    {
        /// <summary>
        /// This property contains the date when the model was created.
        /// </summary>
        [Required]
        DateTime CreatedDate { get; set; }

        /// <summary>
        /// This property contains the name of the user who created the model.
        /// </summary>
        [Required]
        [MaxLength(50)]
        string CreatedBy { get; set; }

        /// <summary>
        /// This property contains the date when the model was last modified.
        /// </summary>
        DateTime? UpdatedDate { get; set; }

        /// <summary>
        /// This property contains the name of the user who last model the upload.
        /// </summary>
        [MaxLength(50)]
        string UpdatedBy { get; set; }
    }
}
