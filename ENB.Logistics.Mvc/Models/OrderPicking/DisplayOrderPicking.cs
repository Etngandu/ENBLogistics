using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ENB.Test.Logistics.Entities;

namespace ENB.Logistics.Mvc.Models
{
    public class DisplayOrderPicking
    {
        public int Id { get; set; }
        public int OperatorId { get; set; }
        public int NContainers { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public ZonePicking ZonePicking { get; set; }
        public TypePicking TypePicking { get; set; }
    }
}