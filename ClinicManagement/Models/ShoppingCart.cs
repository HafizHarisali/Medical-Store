using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClinicManagement.Models
{
    public class ShoppingCart
    {
        public static List<ProductItems> Items = new List<ProductItems>();
    }

    public class ProductItems
    {
        public int Id;
        public int Count;
    }
}