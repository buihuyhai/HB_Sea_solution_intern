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
        public string Order_Id { get; set; }
        public DateTime Order_Date { get; set; }
        public string Customer_Id { get; set; }
        public string Delivery_Status { get; set; }
        public string Shipping_Provider_Id { get; set; }
        public DateTime Over_Due_Date { get; set; }
        public string Payment_Status { get; set; }
        public string Payment_Method_Id { get; set; }
        public DateTime? Paid_At { get; set; }
        public List<Order_Item> Items { get; set; }

        public Order(string order_id, decimal total_amount, string customer_id, string shipping_provider_id, string payment_method_id, string delivery_status, DateTime over_due_date, string payment_status)
            : base(total_amount)
        {
            Order_Id = order_id;
            Order_Date = DateTime.Now;
            Customer_Id = customer_id;
            Shipping_Provider_Id = shipping_provider_id;
            Payment_Method_Id = payment_method_id;
            Total_Amount = total_amount;
            Delivery_Status = delivery_status;
            Over_Due_Date = over_due_date;
            Payment_Status = payment_status;
            Items = new List<Order_Item>();
        }

        public override void Save(MySqlConnection connection)
        {
            string query = "INSERT INTO `Order` (order_id, order_date, total_amount, delivery_status, overdue_date, payment_status, paid_at, customer_id, shipping_provider_id, payment_method_id) " +
                           "VALUES (@OrderId, @OrderDate, @TotalAmount, @DeliveryStatus, @OverdueDate, @PaymentStatus, @PaidAt, @CustomerId, @ShippingProviderId, @PaymentMethodId)";
            using (MySqlCommand cmd = new MySqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@OrderId", Order_Id);
                cmd.Parameters.AddWithValue("@OrderDate", Order_Date);
                cmd.Parameters.AddWithValue("@TotalAmount", Total_Amount);
                cmd.Parameters.AddWithValue("@DeliveryStatus", Delivery_Status);
                cmd.Parameters.AddWithValue("@OverdueDate", Over_Due_Date);
                cmd.Parameters.AddWithValue("@PaymentStatus", Payment_Status);
                cmd.Parameters.AddWithValue("@PaidAt", Paid_At.HasValue ? (object)Paid_At.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@CustomerId", Customer_Id);
                cmd.Parameters.AddWithValue("@ShippingProviderId", Shipping_Provider_Id);
                cmd.Parameters.AddWithValue("@PaymentMethodId", Payment_Method_Id);

                cmd.ExecuteNonQuery();
            }

            foreach (var item in Items)
            {
                item.Save(connection);
            }
        }

    }
}
