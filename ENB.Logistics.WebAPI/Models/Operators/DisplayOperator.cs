using System;
using System.ComponentModel.DataAnnotations;
using ENB.Test.Logistics.Entities;

namespace ENB.Logistics.WebAPI.Models
{
  public class DisplayOperator
  {
    public int Id { get; set; }
   [DisplayFormat(DataFormatString = "{0: MM/dd/yyyy}")]
   public DateTime DateCreated { get; set; }

   [DisplayFormat(DataFormatString = "{0: MM/dd/yyyy}")]
   public DateTime DateModified { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string FullName { get; set; }

    [DisplayFormat(DataFormatString = "{0: MM/dd/yyyy}")]
    public DateTime DateOfBirth { get; set; }
    public OperatorType OpType { get; set; }

    }
}