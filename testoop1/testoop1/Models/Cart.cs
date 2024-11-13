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
        public string CartId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string CustomerId { get; set; }

        public List<ShoppingCart> Itemss { get; set; }

        public Cart(string cartid, decimal totalamount, string customerid) : base(totalamount)
        {
            CartId = cartid;
            TotalAmount = totalamount;
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
            CustomerId = customerid;
            Itemss = new List<ShoppingCart>();
        }
        public override void Save(MySqlConnection connection)
        {
            string query = "INSERT INTO Cart (cart_id, total_amount, created_at, updated_at, customer_id)" +
                           "VALUES (@CartId,@TotalAmount,@CreatedAt,@UpdatedAt,@CustomerId)";
            using (MySqlCommand cmd = new MySqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@CartId", CartId);
                cmd.Parameters.AddWithValue("@TotalAmount", TotalAmount);
                cmd.Parameters.AddWithValue("@CreatedAt", CreatedAt);
                cmd.Parameters.AddWithValue("@UpdatedAt", UpdatedAt);
                cmd.Parameters.AddWithValue("@CustomerId", CustomerId);
                cmd.ExecuteNonQuery();
            }

            foreach (var item in Itemss)
            {
                item.Save(connection);
            }
        }
    }
}
