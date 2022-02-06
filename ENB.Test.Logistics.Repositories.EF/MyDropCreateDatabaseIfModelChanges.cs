using ENB.Test.Logistics.Entities;
using System;
using System.Data.Entity;

namespace ENB.Test.Logistics.Repositories.EF
{
  /// <summary>
  /// A custom implementation of OperatorContext that creates a new contact person with address details.
  /// </summary>
    public class MyDropCreateDatabaseIfModelChanges : DropCreateDatabaseIfModelChanges<OperatorContext>
  {
    /// <summary>
    /// Creates a new contact person.
    /// </summary>
    /// <param name="context">The context to which the new seed data is added.</param>
        protected override void Seed(OperatorContext context)
    {
      var journal = new Operator
      {
        FirstName="Etienne",
        LastName="Ngandu Bukasa",        
        DateOfBirth= new DateTime(1971,7,11),
        OpType = OperatorType.Embedded
        
      };
      context.Operators.Add(journal);
    }

    }
}
