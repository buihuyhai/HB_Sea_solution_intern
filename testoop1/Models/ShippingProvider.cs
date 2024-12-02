using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testoop1.Models
{
    public class ShippingProvider
    {
        public string ShippingProviderId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }

        public ShippingProvider(string shippingproviderid, string name, string address, string phone)
        {
            ShippingProviderId = shippingproviderid;
            Name = name;
            Address = address;
            Phone = phone;
        }
    }
}
