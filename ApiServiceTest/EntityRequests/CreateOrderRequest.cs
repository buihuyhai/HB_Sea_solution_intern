using System;
using System.Collections.Generic;

namespace ApiServiceTest.EntityRequests
{
    public class CreateOrderRequest
    {
        public string CustomerID { get; set; }
        public string ShippingProviderID { get; set; }
        public string PaymentMethodID { get; set; }
        public decimal TotalAmount { get; set; }
        public string DeliveryStatus { get; set; }
        public DateTime? OverdueDate { get; set; }
        public string PaymentStatus { get; set; }
        public DateTime? PaidAt { get; set; }
        public List<CreateOrderItemRequest> OrderItems { get; set; }
    }



}
