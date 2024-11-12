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
        public string Cart_Id { get; set; }
        public string Product_Id { get; set; }
        public int Quantity { get; set; }

        public ShoppingCart(string cart_id, string product_id, int quantity)
        {
            Cart_Id = cart_id;
            Product_Id = product_id;
            Quantity = quantity;
        }

        public void Save(MySqlConnection connection)
        {
            string query = "INSERT INTO ShoppingCart (product_id,cart_id,quantity) " +
                           "VALUES (@Product_Id,@Cart_Id,@Quantity)";
            using (MySqlCommand cmd = new MySqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@Product_Id", Product_Id);
                cmd.Parameters.AddWithValue("@Cart_Id", Cart_Id);
                cmd.Parameters.AddWithValue("@Quantity", Quantity);

                cmd.ExecuteNonQuery();
            }
        }
    }
}
