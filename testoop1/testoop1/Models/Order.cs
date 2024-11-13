using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using testoop1.Abstracts;
using testoop1.Interfaces;
using testoop1.Models;

namespace testoop1.Models
{
    public class Order : Transaction, ISaveable
    {
        public string OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public string CustomerId { get; set; }
        public string DeliveryStatus { get; set; }
        public string ShippingProviderId { get; set; }
        public DateTime OverDueDate { get; set; }
        public string PaymentStatus { get; set; }
        public string PaymentMethodId { get; set; }
        public DateTime? PaidAt { get; set; }
        public List<OrderItem> Items { get; set; }

        public Order(string orderid, decimal totalamount, string customerid, string shippingproviderid, string paymentmethodid, string deliverystatus, DateTime overduedate, string paymentstatus)
            : base(totalamount)
        {
            OrderId = orderid;
            OrderDate = DateTime.Now;
            CustomerId = customerid;
            ShippingProviderId = shippingproviderid;
            PaymentMethodId = paymentmethodid;
            TotalAmount = totalamount;
            DeliveryStatus = deliverystatus;
            OverDueDate = overduedate;
            PaymentStatus = paymentstatus;
            Items = new List<OrderItem>();
        }

        public override void Save(MySqlConnection connection)
        {
            string query = "INSERT INTO `Order` (order_id, order_date, total_amount, delivery_status, overdue_date, payment_status, paid_at, customer_id, shipping_provider_id, payment_method_id) " +
                           "VALUES (@OrderId, @OrderDate, @TotalAmount, @DeliveryStatus, @OverdueDate, @PaymentStatus, @PaidAt, @CustomerId, @ShippingProviderId, @PaymentMethodId)";
            using (MySqlCommand cmd = new MySqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@OrderId", OrderId);
                cmd.Parameters.AddWithValue("@OrderDate", OrderDate);
                cmd.Parameters.AddWithValue("@TotalAmount", TotalAmount);
                cmd.Parameters.AddWithValue("@DeliveryStatus", DeliveryStatus);
                cmd.Parameters.AddWithValue("@OverdueDate", OverDueDate);
                cmd.Parameters.AddWithValue("@PaymentStatus", PaymentStatus);
                cmd.Parameters.AddWithValue("@PaidAt", PaidAt.HasValue ? (object)PaidAt.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@CustomerId", CustomerId);
                cmd.Parameters.AddWithValue("@ShippingProviderId", ShippingProviderId);
                cmd.Parameters.AddWithValue("@PaymentMethodId", PaymentMethodId);

                cmd.ExecuteNonQuery();
            }

            foreach (var item in Items)
            {
                item.Save(connection);
            }
        }

    }
}
