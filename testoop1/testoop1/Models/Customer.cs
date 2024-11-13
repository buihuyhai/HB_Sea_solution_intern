using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testoop1.Models
{
    public class Customer
    {
        public string CustomerId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public string Address { get; set; }
        public string Password { get; set; }
        public DateTime CreatedAt { get; set; }
        public sbyte IsVip { get; set; }

        public Customer(string customerid, string name, string phone, string email, string address, string password, DateTime createdat, sbyte isvip)
        {
            CustomerId = customerid;
            Name = name;
            Phone = phone;
            Email = email;
            Address = address;
            Password = password;
            CreatedAt = createdat;
            IsVip = isvip;
        }
    }
}
