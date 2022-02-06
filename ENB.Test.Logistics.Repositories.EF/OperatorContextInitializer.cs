using System.Data.Entity;

namespace ENB.Test.Logistics.Repositories.EF
{
  /// <summary>
  /// Used to initialize the OperatorContext.
  /// </summary>
  public static class OperatorContextInitializer
  {
        /// <summary>
        /// Sets the IDatabaseInitializer for the application.
        /// </summary>
        /// <param name="dropDatabaseIfModelChanges">When true, uses the MyDropCreateDatabaseIfModelChanges to recreate the database when necessary.
        /// Otherwise, database initialization is disabled by passing null to the SetInitializer method.
        /// </param>
   public static void Init(bool dropDatabaseIfModelChanges)
    {
          if (dropDatabaseIfModelChanges)
          {
            Database.SetInitializer(new MyDropCreateDatabaseIfModelChanges());
            using (var db = new OperatorContext())
            {
              db.Database.Initialize(false);
            }
          }
          else
          {
            Database.SetInitializer<OperatorContext>(null);
          }
    }
  }
}
