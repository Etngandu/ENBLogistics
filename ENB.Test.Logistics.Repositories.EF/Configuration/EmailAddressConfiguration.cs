using System.Data.Entity.ModelConfiguration;
using ENB.Test.Logistics.Entities;

namespace ENB.Test.Logistics.Repositories.EF.Configuration
{
    /// <summary>
    /// Configures the behavior for an e-mail address in the model and the database.
    /// </summary>
    public class EmailAddressConfiguration : EntityTypeConfiguration<EmailAddress>
    {
        /// <summary>
        /// Initializes a new instance of the EmailAddressConfiguration class.
        /// </summary>
        public EmailAddressConfiguration()
        {
            Property(x => x.EmailAddressText).HasMaxLength(250);
        }
    }
}
