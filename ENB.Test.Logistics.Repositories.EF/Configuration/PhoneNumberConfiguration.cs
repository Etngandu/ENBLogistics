using System.Data.Entity.ModelConfiguration;

using ENB.Test.Logistics.Entities;

namespace ENB.Test.Logistics.Repositories.EF.Configuration
{
    /// <summary>
    /// Configures the behavior for a phone number in the model and the database.
    /// </summary>
    public class PhoneNumberConfiguration : EntityTypeConfiguration<PhoneNumber>
    {
        /// <summary>
        /// Initializes a new instance of the PhoneNumberConfiguration class.
        /// </summary>
        public PhoneNumberConfiguration()
        {
             Property(x => x.Number).HasMaxLength(25);
        }
    }
}
