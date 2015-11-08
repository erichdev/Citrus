using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Citrus.Models.Domain
{
    public class VolCategory
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CategoryId { get; set; }
    }
}