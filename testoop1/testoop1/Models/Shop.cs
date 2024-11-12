using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testoop1.Models
{
    public class Shop
    {
        public string Shop_Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime Created_At { get; set; }
        public string User_Id { get; set; }

        public Shop(string shop_id, string name, string address, string phone, string email, DateTime created_at, string user_id)
        {
            Shop_Id = shop_id;
            Name = name;
            Address = address;
            Phone = phone;
            Email = email;
            Created_At = created_at;
            User_Id = user_id;
        }
    }
}
