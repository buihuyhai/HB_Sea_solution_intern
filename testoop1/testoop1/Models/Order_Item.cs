using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testoop1.Models
{
    public class Order_Item
    {
        public string Order_Id { get; set; }
        public string Product_Id { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public Order_Item(string order_Id, string product_Id, int quantity, decimal price)
        {
            Order_Id = order_Id;
            Product_Id = product_Id;
            Quantity = quantity;
            Price = price;
        }
        public void Save(MySqlConnection connection)
        {
            string query = "INSERT INTO Order_Item (order_id, product_id, quantity, price) " +
                           "VALUES (@OrderId, @ProductId, @Quantity, @Price)";
            using (MySqlCommand cmd = new MySqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@OrderId", Order_Id);
                cmd.Parameters.AddWithValue("@ProductId", Product_Id);
                cmd.Parameters.AddWithValue("@Quantity", Quantity);
                cmd.Parameters.AddWithValue("@Price", Price);

                cmd.ExecuteNonQuery();
            }
        }
    }
}
