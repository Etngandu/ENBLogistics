using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ENB.Test.Logistics.Entities.Collections
{
    /// <summary>
    /// Represents a collection of OrderPiking instances in the system.
    /// </summary>
    public class OrderPickings : CollectionBase<OrderPicking>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OrderPickings"/> class.
        /// </summary>
        public OrderPickings() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderPickings"/> class.
        /// </summary>
        /// <param name="initialList">Accepts an IList of EmailAddress as the initial list.</param>
        public OrderPickings(IList<OrderPicking> initialList) : base(initialList) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderPickings"/> class.
        /// </summary>
        /// <param name="initialList">Accepts a CollectionBase of EmailAddress as the initial list.</param>
        public OrderPickings(CollectionBase<OrderPicking> initialList) : base(initialList) { }

        /// <summary>
        /// Adds a new instance of EmailAddress to the collection.
        /// </summary>
        /// <param name="ncontainers">The e-mail address text.</param>
        /// <param name="zonepicking">The type of the e-mail address.</param>
        /// <param name="typepicking">The e-mail address text.</param>
        /// <param name="contactType">The type of the e-mail address.</param>
        ///  <param name="datecreated">The e-mail address text.</param>
        /// <param name="datemodified">The type of the e-mail address.</param>

        public void Add(int ncontainers, ZonePicking zonePicking,TypePicking typePicking, DateTime dateCreated, DateTime dateModified)
        {
            Add(new OrderPicking { NContainers=ncontainers, ZonePicking=zonePicking,TypePicking =typePicking, DateCreated=dateCreated, DateModified=dateModified });
        }

        /// <summary>
        /// Validates the current collection by validating each individual item in the collection.
        /// </summary>
        /// <returns>A IEnumerable of ValidationResult. The IEnumerable is empty when the object is in a valid state.</returns>
        public IEnumerable<ValidationResult> Validate()
        {
            var errors = new List<ValidationResult>();
            foreach (var ncontainers in this)
            {
                errors.AddRange(ncontainers.Validate());
            }
            return errors;
        }
    }
}
