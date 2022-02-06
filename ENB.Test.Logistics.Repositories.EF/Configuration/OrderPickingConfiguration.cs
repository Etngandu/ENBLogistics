using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using ENB.Test.Logistics.Entities;


namespace ENB.Test.Logistics.Repositories.EF.Configuration
{
    /// <summary>
    /// Configures the behavior for an e-mail address in the model and the database.
    /// </summary>
    public class OrderPickingConfiguration : EntityTypeConfiguration<OrderPicking>
    {
        /// <summary>
        /// Initializes a new instance of the OrderPickingConfiguration class.
        /// </summary>
        public OrderPickingConfiguration()
        {
            Property(x => x.TypePicking).HasColumnName("TypePicking");
            Property(x => x.ZonePicking).HasColumnName("ZonePicking");
        }
    }
}
