using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Citrus.Models.Domain
{
    public class Event
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Organization { get; set; }
        public int Category { get; set; }
        public string ZipCode { get; set; }
        public string Description { get; set; }
    }
}