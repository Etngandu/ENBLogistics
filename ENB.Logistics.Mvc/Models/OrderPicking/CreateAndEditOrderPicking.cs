using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ENB.Test.Logistics.Entities;

namespace ENB.Logistics.Mvc.Models
{
    public class CreateAndEditOrderPicking 
    {
        #region "Public Properties"
        /// <summary>
        /// Gets or sets the Id of the OrderPicking.
        /// </summary>
        /// 
        /// <summary>
        /// Gets or sets the Id of the Gastenboek.
        /// </summary>

        public int Id { get; set;  }
        public int NContainers { get; set; }

        public int OperatorId { get; set; }

        /// <summary>
        /// Gets or sets the type of this ZonePicking.
        /// </summary>
        public ZonePicking ZonePicking { get; set; }

        /// <summary>
        /// Gets or sets the type of Picking.
        /// </summary>
        public TypePicking TypePicking { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Determines whether this object is valid or not.
        /// </summary>
        /// <param name="validationContext">Describes the context in which a validation check is performed.</param>
        /// <returns>A IEnumerable of ValidationResult. The IEnumerable is empty when the object is in a valid state.</returns>
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (ZonePicking == ZonePicking.None)
            {
                yield return new ValidationResult("ZonePicking can't be None.", new[] { "ZonePicking" });
            }


            if (TypePicking == TypePicking.None)
            {
                yield return new ValidationResult("TypeMPicking can't be None.", new[] { "TypePicking" });
            }
        }
        #endregion


    }
    
}