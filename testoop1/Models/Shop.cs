using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testoop1.Models
{
    public class Shop
    {
        public string ShopId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; }
        public string UserId { get; set; }

        public Shop(string shopid, string name, string address, string phone, string email, DateTime createdat, string userid)
        {
            ShopId = shopid;
            Name = name;
            Address = address;
            Phone = phone;
            Email = email;
            CreatedAt = createdat;
            UserId = userid;
        }
    }
}
