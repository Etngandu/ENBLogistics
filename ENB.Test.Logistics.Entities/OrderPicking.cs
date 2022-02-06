using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using ENB.Test.Logistics.Infrastructure;

namespace ENB.Test.Logistics.Entities
{
    public class OrderPicking : DomainEntity<int>, IHasOwner, IDateTracking
    {
        #region Properties

        /// <summary>
        /// Gets or sets the number of this NContainers instance.
        /// </summary>
        [Required]
        public int NContainers { get; set; }

        /// <summary>
        /// Gets or sets the type of this ZonePicking.
        /// </summary>
        public ZonePicking ZonePicking { get; set; }

        /// <summary>
        /// Gets or sets the type of Picking.
        /// </summary>
        public TypePicking TypePicking { get; set; }


        /// <summary>
        /// Gets or sets the owner of this phone number.
        /// </summary>
        public Operator Owner { get; set; }

        /// <summary>
        /// Gets or sets the ID of the owner of this Order picking.
        /// </summary>
        public int OwnerId { get; set; }

        /// <summary>
        /// Gets or sets the date and time the object was created.
        /// </summary>
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// Gets or sets the date and time the object was last modified.
        /// </summary>
        public DateTime DateModified { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Determines whether this object is valid or not.
        /// </summary>
        /// <param name="validationContext">Describes the context in which a validation check is performed.</param>
        /// <returns>A IEnumerable of ValidationResult. The IEnumerable is empty when the object is in a valid state.</returns>
        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
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
