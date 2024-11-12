using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testoop1.Models
{
    public class Customer
    {
        public string Customer_Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public string Address { get; set; }
        public string Password { get; set; }
        public DateTime Created_At { get; set; }
        public sbyte IsVip { get; set; }

        public Customer(string customer_id, string name, string phone, string email, string address, string password, DateTime created_at, sbyte isvip)
        {
            Customer_Id = customer_id;
            Name = name;
            Phone = phone;
            Email = email;
            Address = address;
            Password = password;
            Created_At = created_at;
            IsVip = isvip;
        }
    }
}
