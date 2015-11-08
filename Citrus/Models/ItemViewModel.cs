using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Citrus.Models
{
    public class ItemViewModel<T>
    {
        public T Item { get; set; }
    }
}