DROP TABLE IF EXISTS ShoppingCart;
DROP TABLE IF EXISTS Cart;
DROP TABLE IF EXISTS Order_Item;
DROP TABLE IF EXISTS Product;
DROP TABLE IF EXISTS `Order`;
DROP TABLE IF EXISTS Payment_method;
DROP TABLE IF EXISTS Shop;
DROP TABLE IF EXISTS Customer;
DROP TABLE IF EXISTS ShippingProvider;
DROP PROCEDURE IF EXISTS CreateOrder;
DROP PROCEDURE IF EXISTS CancelOverdueOrders;
DROP PROCEDURE IF EXISTS VipCustomers;

CREATE TABLE Payment_method (
    payment_method_id VARCHAR(100) PRIMARY KEY,
    name VARCHAR(100) NOT NULL
);

CREATE TABLE Customer (
    Customer_id VARCHAR(100) PRIMARY KEY,
    name VARCHAR(30) NOT NULL,
    phone VARCHAR(30),
    address VARCHAR(30),
    email VARCHAR(30),
    password VARCHAR(30),
    created_at DATE,
    isVip TINYINT
);

CREATE TABLE ShippingProvider (
    shipping_provider_id VARCHAR(100) PRIMARY KEY,
    name VARCHAR(30) NOT NULL,
    address VARCHAR(30),
    phone VARCHAR(30)
);

CREATE TABLE `Order` (
    order_id VARCHAR(100) PRIMARY KEY,
    order_date DATE NOT NULL,
    total_amount DECIMAL(15, 0) NOT NULL,
    delivery_status VARCHAR(30) NOT NULL,
    overdue_date DATE,
    payment_status VARCHAR(30) NOT NULL,
    paid_at DATE,
    Customer_id VARCHAR(100),
    Shipping_Provider_id VARCHAR(100),
    Payment_method_id VARCHAR(100),
    FOREIGN KEY (Customer_id) REFERENCES Customer(Customer_id),
    FOREIGN KEY (Shipping_Provider_id) REFERENCES ShippingProvider(shipping_provider_id),
    FOREIGN KEY (Payment_method_id) REFERENCES Payment_method(payment_method_id)
);

CREATE TABLE Shop (
    shop_id VARCHAR(100) PRIMARY KEY,
    name VARCHAR(30) NOT NULL,
    address VARCHAR(30),
    phone VARCHAR(30),
    email VARCHAR(30),
    created_at DATE NOT NULL,
    Customer_id VARCHAR(100)
);

CREATE TABLE Product (
    product_id VARCHAR(100) PRIMARY KEY,
    name VARCHAR(30) NOT NULL,
    description VARCHAR(1000),
    price DECIMAL(15, 0) NOT NULL,
    stock INT,
    created_at DATE,
    Shop_id VARCHAR(100),
    FOREIGN KEY (Shop_id) REFERENCES Shop(shop_id)
);

CREATE TABLE Cart (
    cart_id VARCHAR(100) PRIMARY KEY,
    total_amount DECIMAL(15, 0),
    created_at DATE,
    updated_at DATE,
    Customer_id VARCHAR(100),
    FOREIGN KEY (Customer_id) REFERENCES Customer(Customer_id)
);

CREATE TABLE Order_Item (
    Order_id VARCHAR(100),
    Product_id VARCHAR(100),
    quantity INT,
    price DECIMAL(15, 0),
    PRIMARY KEY (Order_id, Product_id),
    FOREIGN KEY (Order_id) REFERENCES `Order`(order_id),
    FOREIGN KEY (Product_id) REFERENCES Product(product_id)
);

CREATE TABLE ShoppingCart (
    product_id VARCHAR(100),
    cart_id VARCHAR(100),
    quantity INT,
    PRIMARY KEY (product_id, Cart_id),
    FOREIGN KEY (product_id) REFERENCES Product(product_id),
    FOREIGN KEY (Cart_id) REFERENCES Cart(cart_id)
);


INSERT INTO Payment_method (payment_method_id, name) VALUES
('pm_001', 'COD'),
('pm_002', 'Credit Card'),
('pm_003', 'Bank Transfer'),
('pm_004', 'PayPal'),
('pm_005', 'Bitcoin');

INSERT INTO Customer (Customer_id, name, phone, address, email, password, created_at, isVip) VALUES
('Customer_001', 'John Doe', '123456789', '123 Main St', 'johndoe@example.com', 'password123', NOW(), 0),
('Customer_002', 'Jane Smith', '987654321', '456 Oak St', 'janesmith@example.com', 'password123', NOW(), 0),
('Customer_003', 'Alice Brown', '1122334455', '789 Pine St', 'alicebrown@example.com', 'password123', NOW(), 0),
('Customer_004', 'Bob Johnson', '5566778899', '101 Maple St', 'bobjohnson@example.com', 'password123', NOW(), 0),
('Customer_005', 'Eve Davis', '9988776655', '202 Birch St', 'evedavis@example.com', 'password123', NOW(), 0);

INSERT INTO ShippingProvider (shipping_provider_id, name, address, phone) VALUES
('ship_001', 'FedEx', '123 ShippingProvider St', '111222333'),
('ship_002', 'UPS', '456 Delivery Ave', '444555666'),
('ship_003', 'DHL', '789 Courier Blvd', '777888999'),
('ship_004', 'USPS', '101 Postal Rd', '000111222'),
('ship_005', 'Amazon Logistics', '202 Prime Ln', '333444555');

INSERT INTO `Order` (order_id, order_date, total_amount, delivery_status, overdue_date, payment_status, paid_at, Customer_id, Shipping_Provider_id, Payment_method_id) VALUES
('order_001', '2023-11-01', 50000, 'Đang vận chuyển', '2023-11-05', 'Pending', NULL, 'Customer_001', 'ship_001', 'pm_001'),
('order_002', '2023-11-02', 1000001, 'Đang kiểm hàng', '2023-11-06', 'Pending', NULL, 'Customer_002', 'ship_002', 'pm_002'),
('order_003', '2023-11-03', 1500000, 'Đã giao hàng', '2023-11-07', 'Paid', '2023-11-03', 'Customer_003', 'ship_003', 'pm_003'),
('order_004', '2023-11-04', 200000, 'Hủy bỏ', '2023-11-08', 'Refunded', NULL, 'Customer_004', 'ship_004', 'pm_004'),
('order_005', '2023-11-05', 2500000, 'Chưa giao hàng', '2023-11-03', 'Pending', NULL, 'Customer_005', 'ship_005', 'pm_005');

INSERT INTO Shop (shop_id, name, address, phone, email, created_at, Customer_id) VALUES
('shop_001', 'Electronics World', '123 Tech St', '111222333', 'contact@electroworld.com', NOW(), 'Customer_004'),
('shop_002', 'Book Haven', '456 Literature Ave', '444555666', 'info@bookhaven.com', NOW(), 'Customer_001'),
('shop_003', 'Fashion Hub', '789 Style Blvd', '777888999', 'support@fashionhub.com', NOW(), 'Customer_002'),
('shop_004', 'Home Goods', '101 Home Rd', '000111222', 'service@homegoods.com', NOW(), 'Customer_003'),
('shop_005', 'Toy Kingdom', '202 Fun Ln', '333444555', 'hello@toykingdom.com', NOW(), 'Customer_005');

INSERT INTO Product (product_id, name, description, price, stock, created_at, Shop_id) VALUES
('item_001', 'Smartphone', 'Latest model smartphone with advanced features', 50000, 100, NOW(), 'shop_001'),
('item_002', 'Laptop', 'High-performance laptop for gaming and work', 1000000, 50, NOW(), 'shop_001'),
('item_003', 'Book', 'Bestselling novel with an intriguing plot', 1500000, 200, NOW(), 'shop_002'),
('item_004', 'T-shirt', 'Comfortable cotton t-shirt in various colors', 20000, 300, NOW(), 'shop_003'),
('item_005', 'Toy Car', 'Battery-operated toy car for kids', 1000001, 150, NOW(), 'shop_005');

INSERT INTO Cart (cart_id, total_amount, created_at, updated_at, Customer_id) VALUES
('cart_001', 50000, NOW(), NOW(), 'Customer_001'),
('cart_002', 100000, NOW(), NOW(), 'Customer_002'),
('cart_003', 150000, NOW(), NOW(), 'Customer_003'),
('cart_004', 200000, NOW(), NOW(), 'Customer_004'),
('cart_005', 250000, NOW(), NOW(), 'Customer_005');

INSERT INTO Order_Item (Order_id, Product_id, quantity, price) VALUES
('order_001', 'item_001', 1, 50000),
('order_002', 'item_002', 1, 1000000),
('order_003', 'item_003', 10, 150000),
('order_004', 'item_004', 5, 100000),
('order_005', 'item_005', 25, 250000);

INSERT INTO ShoppingCart (product_id, cart_id, quantity) VALUES
('item_001', 'cart_001', 1),
('item_002', 'cart_002', 1),
('item_003', 'cart_003', 10),
('item_004', 'cart_004', 5),
('item_005', 'cart_005', 25);


SELECT*FROM ShoppingCart;
SELECT*FROM Cart;
SELECT*FROM Order_Item;
SELECT*FROM Product;
SELECT*FROM `Order`;
SELECT*FROM Payment_method;
SELECT*FROM Shop;
SELECT*FROM Customer;
SELECT*FROM ShippingProvider;

-- Cau1
SELECT 
    o.order_id AS "Mã Đơn", 
    u.name AS "Tên Khách Hàng", 
    s.name AS "Nhà Vận Chuyển", 
    o.total_amount AS "Tổng Giá Trị Đơn", 
    o.delivery_status AS "Trạng Thái Đơn Hàng"
FROM `Order` o
JOIN Customer u ON o.Customer_id = u.Customer_id
JOIN ShippingProvider s ON o.shipping_provider_id = s.shipping_provider_id;

-- Cau2
DELIMITER //

CREATE PROCEDURE CreateOrder(
    IN p_Customer_id VARCHAR(100),
    IN p_shipping_provider_id VARCHAR(100),
    IN p_payment_method_id VARCHAR(100),
    IN p_items JSON,
    IN p_delivery_status VARCHAR(30)
)
BEGIN
    DECLARE v_order_id VARCHAR(100);
    DECLARE v_overdue_date DATE;
    DECLARE v_payment_status VARCHAR(30);
    DECLARE v_paid_at DATE;
    DECLARE v_total_amount DECIMAL(15, 0) DEFAULT 0;

    SET v_order_id = CONCAT('O', UUID());
    SET v_overdue_date = DATE_ADD(CURDATE(), INTERVAL 7 DAY);
    SET v_payment_status = 'Pending';
    SET v_paid_at = NULL;

    SELECT SUM(JSON_UNQUOTE(JSON_EXTRACT(item, '$.quantity')) * JSON_UNQUOTE(JSON_EXTRACT(item, '$.price')))
    INTO v_total_amount
    FROM JSON_TABLE(p_items, '$[*]' COLUMNS (item JSON PATH '$')) AS items;

    INSERT INTO `Order` (order_id, order_date, total_amount, delivery_status, overdue_date, payment_status, paid_at, Customer_id, shipping_provider_id, payment_method_id)
    VALUES (v_order_id, CURDATE(), v_total_amount, p_delivery_status, v_overdue_date, v_payment_status, v_paid_at, p_Customer_id, p_shipping_provider_id, p_payment_method_id);

    INSERT INTO Order_Item (order_id, product_id, quantity, price)
    SELECT v_order_id, JSON_UNQUOTE(JSON_EXTRACT(item, '$.product_id')), 
           JSON_UNQUOTE(JSON_EXTRACT(item, '$.quantity')), 
           JSON_UNQUOTE(JSON_EXTRACT(item, '$.price'))
    FROM JSON_TABLE(p_items, '$[*]' COLUMNS (item JSON PATH '$')) AS items;
END //

DELIMITER ;

CALL CreateOrder(
    'Customer_002',           
    'ship_001',           
    'pm_001',          
    '[{"product_id": "item_002", "quantity": 2, "price": 10000}, {"product_id": "item_003", "quantity": 1, "price": 5000}]',        
    'Chưa giao hàng'  
);

SELECT * FROM `Order`;
SELECT * FROM Order_Item;

-- Cau3

DELIMITER //
CREATE PROCEDURE CancelOverdueOrders()
BEGIN
    UPDATE `Order` o
    JOIN Order_Item oi ON o.order_id = oi.order_id
    SET o.delivery_status = 'Hủy bỏ', oi.price = 0,o.payment_status='Refunded'
    WHERE o.delivery_status = 'Chưa giao hàng'
      AND o.overdue_date < CURDATE();
END //
DELIMITER ;
CALL CancelOverdueOrders;
SELECT*FROM `Order`;
SELECT*FROM order_item;

-- Cau4
DELIMITER //
CREATE PROCEDURE VipCustomers()
BEGIN
    UPDATE Customer u
    SET u.isVip = 1
    WHERE u.Customer_id IN (
        SELECT o.Customer_id
        FROM `Order` o
        JOIN Order_Item oi ON o.order_id = oi.order_id
        GROUP BY o.Customer_id
        HAVING SUM(o.total_amount) > 1000000 OR COUNT(DISTINCT oi.product_id) > 10
    );
END //
DELIMITER ;
CALL VipCustomers;
SELECT*FROM Customer;


