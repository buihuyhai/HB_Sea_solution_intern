using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testoop1.Models
{
    public class ShoppingCart 
    {
        public string CartId { get; set; }
        public string ProductId { get; set; }
        public int Quantity { get; set; }

        public ShoppingCart(string cartid, string productid, int quantity)
        {
            CartId = cartid;
            ProductId = productid;
            Quantity = quantity;
        }

        public void Save(MySqlConnection connection)
        {
            string query = "INSERT INTO ShoppingCart (product_id,cart_id,quantity) " +
                           "VALUES (@ProductId,@CartId,@Quantity)";
            using (MySqlCommand cmd = new MySqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@ProductId", ProductId);
                cmd.Parameters.AddWithValue("@CartId", CartId);
                cmd.Parameters.AddWithValue("@Quantity", Quantity);

                cmd.ExecuteNonQuery();
            }
        }
    }
}
