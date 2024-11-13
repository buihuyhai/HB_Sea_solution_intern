using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testoop1.Models
{
    public class Product
    {
        public string ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ShopId { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public DateTime CreatedAt { get; set; }

        public Product(string productid, string name, string description, string shopid, decimal price, int stock, DateTime createdat)
        {
            ProductId = productid;
            Name = name;
            Description = description;
            ShopId = shopid;
            Stock = stock;
            Price = price;
            CreatedAt = createdat;
        }
    }
}
