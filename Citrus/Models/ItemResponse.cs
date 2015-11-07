using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Citrus.Models
{
    public class ItemResponse<T> : SuccessResponse
    {

        public T Item { get; set; }

    }

    public class ItemsResponse<T> : SuccessResponse
    {
        public List<T> Items { get; set; }
    }
}