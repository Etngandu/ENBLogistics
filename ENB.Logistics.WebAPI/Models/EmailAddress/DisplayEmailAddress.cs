using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ENB.Test.Logistics.Entities;

namespace ENB.Logistics.WebAPI.Models
{
    public class DisplayEmailAddress
    {
        public int Id { get; set; }
        public int OperatorId { get; set; }
        public string EmailAddressText { get; set; }
        public ProfileType ProfileType { get; set; }
    }
}