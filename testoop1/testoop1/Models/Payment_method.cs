using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testoop1.Models
{
    public class Payment_method
    {
        public string Payment_Method_Id { get; set; }
        public string Name { get; set; }

        public Payment_method(string payment_method_id, string name)
        {
            Payment_Method_Id = payment_method_id;
            Name = name;
        }
    }
}
