using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using testoop1.Abstracts;
using testoop1.Interfaces;

namespace testoop1.Models
{
    public class Cart : Transaction, ISaveable
    {
        public string Cart_Id { get; set; }
        public DateTime Created_At { get; set; }
        public DateTime Updated_At { get; set; }
        public string Customer_Id { get; set; }

        public List<ShoppingCart> Itemss { get; set; }

        public Cart(string cart_id, decimal total_amount, string customer_id) : base(total_amount)
        {
            Cart_Id = cart_id;
            Total_Amount = total_amount;
            Created_At = DateTime.Now;
            Updated_At = DateTime.Now;
            Customer_Id = customer_id;
            Itemss = new List<ShoppingCart>();
        }
        public override void Save(MySqlConnection connection)
        {
            string query = "INSERT INTO Cart (cart_id, total_amount, created_at, updated_at, customer_id)" +
                           "VALUES (@Cart_Id,@Total_Amount,@Created_At,@Updated_At,@Customer_Id)";
            using (MySqlCommand cmd = new MySqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@Cart_Id", Cart_Id);
                cmd.Parameters.AddWithValue("@Total_Amount", Total_Amount);
                cmd.Parameters.AddWithValue("@Created_At", Created_At);
                cmd.Parameters.AddWithValue("@Updated_At", Updated_At);
                cmd.Parameters.AddWithValue("@Customer_Id", Customer_Id);
                cmd.ExecuteNonQuery();
            }

            foreach (var item in Itemss)
            {
                item.Save(connection);
            }
        }
    }
}
