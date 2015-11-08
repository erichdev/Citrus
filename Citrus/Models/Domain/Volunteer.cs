using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Citrus.Models.Domain
{
    public class Volunteer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public string Phone { get; set; }
    }
}