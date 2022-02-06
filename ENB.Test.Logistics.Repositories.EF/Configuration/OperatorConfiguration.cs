using System.Data.Entity.ModelConfiguration;
using ENB.Test.Logistics.Entities;

namespace ENB.Test.Logistics.Repositories.EF.Configuration
{
  /// <summary>
  /// Configures the behavior for an oper&tor in the model and the database.
  /// </summary>
  public class OperatorConfiguration : EntityTypeConfiguration<Operator>
  {
    /// <summary>
    /// Initializes a new instance of the PersonConfiguration class.
    /// </summary>
      public OperatorConfiguration()
    {
            Property(x => x.FirstName).IsRequired().HasMaxLength(25);
            Property(x => x.LastName).IsRequired().HasMaxLength(40);            

            
        }
  }
}
