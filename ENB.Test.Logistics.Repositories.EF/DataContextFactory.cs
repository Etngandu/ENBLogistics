using ENB.Test.Logistics.Infrastructure.DataContextStorage;


namespace ENB.Test.Logistics.Repositories.EF
{
  /// <summary>
  /// Manages instances of the COpreratorContext and stores them in an appropriate storage container.
  /// </summary>
  public static class DataContextFactory
  {
    /// <summary>
    /// Clears out the current ContactManagerContext.
    /// </summary>
    public static void Clear()
    {
        var dataContextStorageContainer = DataContextStorageFactory<OperatorContext>.CreateStorageContainer();
      dataContextStorageContainer.Clear();
    }

    /// <summary>
    /// Retrieves an instance of ContactManagerContext from the appropriate storage container or
    /// creates a new instance and stores that in a container.
    /// </summary>
    /// <returns>An instance of ContactManagerContext.</returns>
    public static OperatorContext GetDataContext()
    {
        var dataContextStorageContainer = DataContextStorageFactory<OperatorContext>.CreateStorageContainer();
      var operatorContext = dataContextStorageContainer.GetDataContext();
      if (operatorContext == null)
      {
          operatorContext = new OperatorContext();
        dataContextStorageContainer.Store(operatorContext);
      }
      return operatorContext;
    }
  }
}
