using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using ENB.Test.Logistics.Entities;

namespace ENB.Logistics.Mvc.Models
{
  public class CreateAndEditOperator : IValidatableObject
  {
   
    #region "Public Properties"
    /// <summary>
    /// Gets or sets the Id of the Gastenboek.
    /// </summary>
    
    public int Id
    {
        get;
        set;
    }
    /// <summary>
    /// Gets or sets the Name of the Gastenboek.
    /// </summary>
    [Required, DisplayName("First Name")]
    public string FirstName
    {
        get;
        set;
    }
    /// <summary>
    /// 
    /// </summary>
    [Required]    
    [Display(Name = "Last Name")]
    public string LastName
    {
        get;
        set;
    }
      
    /// <summary>
    /// Gets or sets the PublicatieDatum of the Gastenboek.
    /// </summary>
    [DisplayName("Date of birth")]
    public DateTime DateOfBirth
    {
        get;
        set;
    }

        #endregion

        public OperatorType OpType { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (OpType == OperatorType.None)
            {
                yield return new ValidationResult("PersonType can't be None.", new[] { "Type" });
            }
           
        }
    }
}
