using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testoop1.Models
{
    public class PaymentMethod
    {
        public string PaymentMethodId { get; set; }
        public string Name { get; set; }

        public PaymentMethod(string paymentmethodid, string name)
        {
            PaymentMethodId = paymentmethodid;
            Name = name;
        }
    }
}
