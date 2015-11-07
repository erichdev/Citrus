using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Citrus.Models.Domain
{
    public class Volunteer
    {
        public string Name { get; set; }
        public string ZipCode { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
    }
}