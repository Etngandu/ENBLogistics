using System.Linq;
using System.Collections.Generic;
using ENB.Test.Logistics.Entities;
using ENB.Test.Logistics.Infrastructure;
using ENB.Test.Logistics.Entities.Repositories;



namespace ENB.Test.Logistics.Repositories.EF
{
  /// <summary>
  /// A concrete repository to work with operator in the system.
  /// </summary>
    public class OperatorRepository : Repository<Operator>, IOperatorRepository
  {
    /// <summary>
    /// Gets a list of all operator whose last name exactly matches the search string.
    /// </summary>
    /// <param name="name">The last name that the system should search for.</param>
    /// <returns>An IEnumerable of Person with the matching people.</returns>
    public IEnumerable<Operator> FindByName(string lastname)
    {
      return DataContextFactory.GetDataContext().Set<Operator>().Where(x => x.LastName == lastname);
    }
  }
}


