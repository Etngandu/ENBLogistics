using System.Collections.Generic;
using ENB.Test.Logistics.Infrastructure;

namespace ENB.Test.Logistics.Entities.Repositories
{
  /// <summary>
  /// Defines the various methods available to work with people in the system.
  /// </summary>
  public interface IOperatorRepository : IRepository<Operator, int>
  {
    /// <summary>
    /// Gets a list of all the people whose last name exactly matches the search string.
    /// </summary>
    /// <param name="name">The last name that the system should search for.</param>
    /// <returns>An IEnumerable of Person with the matching people.</returns>
    IEnumerable<Operator> FindByName(string name);
  }
}
