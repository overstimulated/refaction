using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace refactor_me.DTO
{
    public class ProductDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public decimal DeliveryPrice { get; set; }
    }

    /// <summary>
    /// wrapper for Items[]
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ItemLists<T> where T: class
    {
        public IEnumerable<T> Items { get; set; }
    }
}