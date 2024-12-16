using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiServiceTest.EntityRequests
{
    public class CreateOrderItemRequest
    {
        public string ProductID { get; set; }
        public int Quantity { get; set; }
    }

}