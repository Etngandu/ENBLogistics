using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ENB.Test.Logistics.Entities;

namespace ENB.Logistics.WebAPI.Models
{
    public class CreateAndEditEmailAddress
    {
        public int Id { get; set; }
        public int OperatorId { get; set; }
        [Required]
        [EmailAddress]
        public string EmailAddressText { get; set; }
        public ProfileType ProfileType { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (ProfileType == ProfileType.None)
            {
                yield return new ValidationResult("ProfileType can't be None.", new[] { "ProfileType" });
            }
        }
    }
}