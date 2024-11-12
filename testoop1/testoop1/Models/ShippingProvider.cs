using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testoop1.Models
{
    public class ShippingProvider
    {
        public string Shipping_Provider_Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }

        public ShippingProvider(string shipping_provider_id, string name, string address, string phone)
        {
            Shipping_Provider_Id = shipping_provider_id;
            Name = name;
            Address = address;
            Phone = phone;
        }
    }
}
