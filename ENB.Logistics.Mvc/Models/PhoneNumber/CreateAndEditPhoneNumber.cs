using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using ENB.Test.Logistics.Entities;

namespace ENB.Logistics.Mvc.Models
{
    public class CreateAndEditPhoneNumber: IValidatableObject
    {

        public int Id { get; set; }

        public int OperatorId { get; set; }
        [Required]
        public string Number { get; set; }
        public ProfileType ProfileType { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (ProfileType == ProfileType.None)
            {
                yield return new ValidationResult("Profiletype can't be None.", new[] { "ProfileTyoe" });
            }
        }
    }
}