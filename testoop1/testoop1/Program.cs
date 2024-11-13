

using testoop1;
using testoop1.Handlers;
using testoop1.Models;



string connectionString = "server=localhost;user=root;password='';database=sqltest1";



Cart cart = new Cart("cart_006", 555555, "Customer_001");
cart.Itemss.Add(new ShoppingCart(cart.CartId, "item_003", 22));
Order order = new Order("order_006", 50000, "Customer_002", "ship_002", "pm_001", "Đang vận chuyển", DateTime.ParseExact("2024-11-05", "yyyy-MM-dd", null), "Pending");
order.Items.Add(new OrderItem("order_006", "item_001", 1, 50000));





TransactionHandler handler = new TransactionHandler(connectionString);
try
{
    handler.SaveTransaction(cart);
    handler.SaveTransaction(order);
}
catch (Exception ex)
{
    Console.WriteLine("Error: " + ex.Message);
}
