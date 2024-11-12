using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testoop1.Models
{
    public class Product
    {
        public string Product_Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Shop_Id { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public DateTime Created_At { get; set; }

        public Product(string product_id, string name, string description, string shop_id, decimal price, int stock, DateTime created_at)
        {
            Product_Id = product_id;
            Name = name;
            Description = description;
            Shop_Id = shop_id;
            Stock = stock;
            Price = price;
            Created_At = created_at;
        }
    }
}
