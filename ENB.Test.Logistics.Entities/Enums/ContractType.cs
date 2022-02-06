using System.ComponentModel.DataAnnotations;

namespace ENB.Test.Logistics.Entities
{
  /// <summary>
  /// Determines the type of a contact record.
  /// </summary>
  public enum ContractType
  {
    /// <summary>
    /// Indicates an unidentified value.
    /// </summary>
    None = 0,

    /// <summary>
    /// Indicates a business contact record.
    /// </summary>
    Temping = 1,

    /// <summary>
    /// Indicates a personal contact record.
    /// </summary>
    Permanent  = 2,

    /// <summary>
    /// Indicates a personal contact record.
    /// </summary>
    [Display(Name = "Student jobs")]
     Student_jobs = 3


    }
}
