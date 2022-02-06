﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Data.Entity.Core;
using System.Linq;
using System.Text;
using ENB.Test.Logistics.Entities;
using ENB.Test.Logistics.Infrastructure;
using ENB.Test.Logistics.Repositories.EF.Configuration;



namespace ENB.Test.Logistics.Repositories.EF
{
  /// <summary>
  /// This is the main DbContext to work with data in the database.
  /// </summary>
  public class OperatorContext : DbContext
  {
        /// <summary>
        /// Initializes a new instance of the DbGastenboekContext class.
        /// </summary>
        public OperatorContext()
          : base("OperatorContext")
    {
      Configuration.LazyLoadingEnabled = false;
    }

    /// <summary>
    /// Provides access to the collection of operator in the system.
    /// </summary>
    public DbSet<Operator> Operators { get; set; }

    /// <summary>
    /// Hooks into the Save process to get a last-minute chance to look at the entities and change them. Also intercepts exceptions and 
    /// wraps them in a new Exception type.
    /// </summary>
    /// <returns>The number of affected rows.</returns>
    public override int SaveChanges()
    {
      // Need to manually delete all "owned objects" that have been removed from their owner, otherwise they'll be orphaned.
      var orphanedObjects = ChangeTracker.Entries().Where(
        e => (e.State ==  EntityState.Modified || e.State == EntityState.Added) &&
          e.Entity is IHasOwner &&
            e.Reference("Owner").CurrentValue == null);

            var rrr = ChangeTracker.Entries().Where(
     e => e.State == EntityState.Modified || e.State == EntityState.Added).ToList();

            foreach (var orphanedObject in orphanedObjects)
      {
        orphanedObject.State = EntityState.Deleted;
      }

      try
      {
        var modified = ChangeTracker.Entries().Where(e => e.State == EntityState.Modified || e.State == EntityState.Added);
        foreach (DbEntityEntry item in modified)
        {
          var changedOrAddedItem = item.Entity as IDateTracking;
          if (changedOrAddedItem != null)
          {
            if (item.State == EntityState.Added)
            {
              changedOrAddedItem.DateCreated = DateTime.Now;
            }
            changedOrAddedItem.DateModified = DateTime.Now;
          }
        }
        return base.SaveChanges();
      }
      catch (DbEntityValidationException entityException)
      {
        var errors = entityException.EntityValidationErrors;
        var result = new StringBuilder();
        var allErrors = new List<ValidationResult>();
        foreach (var error in errors)
        {
          foreach (var validationError in error.ValidationErrors)
          {
            result.AppendFormat("\r\n  Entity of type {0} has validation error \"{1}\" for property {2}.\r\n", error.Entry.Entity.GetType().ToString(), validationError.ErrorMessage, validationError.PropertyName);
            var domainEntity = error.Entry.Entity as DomainEntity<int>;
            if (domainEntity != null)
            {
              result.Append(domainEntity.IsTransient() ? "  This entity was added in this session.\r\n" : string.Format("  The Id of the entity is {0}.\r\n", domainEntity.Id));
            }
            allErrors.Add(new ValidationResult(validationError.ErrorMessage, new[] { validationError.PropertyName }));
          }
        }
        throw new ModelValidationException(result.ToString(), entityException, allErrors);
      }
    }

    /// <summary>
    /// Configures the EF context.
    /// </summary>
    /// <param name="modelBuilder">The model builder that needs to be configured.</param>
    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
        modelBuilder.Configurations.Add(new OperatorConfiguration());
        modelBuilder.Configurations.Add(new EmailAddressConfiguration());
        modelBuilder.Configurations.Add(new PhoneNumberConfiguration());
        modelBuilder.Configurations.Add(new OrderPickingConfiguration());   
    }
       
    }
}