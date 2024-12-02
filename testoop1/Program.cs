

using Singletons;
using testoop1;
using testoop1.Facades;
using testoop1.Factories;
using testoop1.Handlers;
using testoop1.Models;


DatabaseConnection.Initialize("server=localhost;user=root;password='';database=sqltest1");


Cart cart = (Cart)TransactionFactory.CreateTransaction("cart", "cart_006", 555555m, "Customer_001");
cart.Itemss.Add(new ShoppingCart(cart.CartId, "item_003", 22));

Order order = (Order)TransactionFactory.CreateTransaction(
    "order", "order_006", 50000m, "Customer_002","ship_002", "pm_001", "Đang vận chuyển",
    DateTime.ParseExact("2024-11-05", "yyyy-MM-dd", null),"Pending");
order.Items.Add(new OrderItem("order_006", "item_001", 1, 50000m));


TransactionFacade facade = new TransactionFacade();
facade.SaveTransaction(cart);
facade.SaveTransaction(order);
