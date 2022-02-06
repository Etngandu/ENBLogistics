using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using ENB.Test.Logistics.Infrastructure;

namespace ENB.Test.Logistics.Entities
{
    /// <summary>
    /// Represents a phone number in the system.
    /// </summary>
    public class PhoneNumber : DomainEntity<int>, IHasOwner
    {
        #region Properties

        /// <summary>
        /// Gets or sets the number of this PhoneNumber instance.
        /// </summary>
        [Required]
        public string Number { get; set; }

        /// <summary>
        /// Gets or sets the type of this phone number.
        /// </summary>
        public ProfileType ProfileType { get; set; }

        /// <summary>
        /// Gets or sets the owner of this phone number.
        /// </summary>
        public Operator Owner { get; set; }

        /// <summary>
        /// Gets or sets the ID of the owner of this phone number.
        /// </summary>
        public int OwnerId { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Determines whether this object is valid or not.
        /// </summary>
        /// <param name="validationContext">Describes the context in which a validation check is performed.</param>
        /// <returns>A IEnumerable of ValidationResult. The IEnumerable is empty when the object is in a valid state.</returns>
        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (ProfileType == ProfileType.None)
            {
                yield return new ValidationResult("ContactType can't be None.", new[] { "ContactType" });
            }
        }
        #endregion
    }
}
