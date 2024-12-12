using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiServiceTest.EntityRespones
{
    public class OrderDetailResponse
    {
        public string OrderID { get; set; }
        public int ItemCount { get; set; }
        public decimal TotalAmount { get; set; }
        public string ShopName { get; set; }
        public DateTime EstimatedDeliveryDate { get; set; }
        public string DeliveryStatus { get; set; }

        public List<OrderItemResponse> OrderItems { get; set; }
    }
}