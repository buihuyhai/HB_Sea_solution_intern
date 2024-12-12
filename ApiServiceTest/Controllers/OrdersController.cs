using ApiServiceTest.EntityRequests;
using ApiServiceTest.EntityRespones;
using ApiServiceTest.Models;
using ApiServiceTest.Services;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;

namespace ApiServiceTest.Controllers
{
    [RoutePrefix("api/v1")]
    public class OrdersController : ApiController
    {
        private readonly OrderServices _orderServices;
        private static readonly ILog _logger = LogManager.GetLogger(typeof(OrdersController));

        public OrdersController()
        {
            _orderServices = new OrderServices(new TestApiDBEntities());
        }

        [HttpGet]
        [Route("orders")]
        public async Task<IHttpActionResult> GetOrders()
        {

            try
            {
                List<OrderResponse> orders = await _orderServices.GetOrdersAsync();

                if (orders == null || orders.Count == 0)
                {
                    _logger.Warn("No orders found");
                    return NotFound();
                }

                return Ok(orders);
            }
            catch (Exception ex)
            {
                _logger.Error("An error occurred while retrieving orders", ex);
                return InternalServerError(ex);
            }
        }


        [HttpGet]
        [Route("orderdetail")]
        public async Task<IHttpActionResult> GetOrderDetails(string orderId)
        {

            try
            {
                var orderDetails = await _orderServices.GetOrderDetailsAsync(orderId);

                if (orderDetails == null)
                {
                    _logger.Warn($"No order details found for OrderId: {orderId}");
                    return NotFound();
                }

                return Ok(orderDetails);
            }
            catch (Exception ex)
            {
                _logger.Error($"An error occurred while retrieving details for OrderId: {orderId}", ex);
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        [Route("orders")]
        public async Task<IHttpActionResult> CreateOrder(CreateOrderRequest request)
        {

            try
            {
                if (request == null || request.OrderItems == null || !request.OrderItems.Any())
                {
                    _logger.Error("Invalid order request. Order and order items are required.");
                    return BadRequest("Invalid order request. Order and order items are required.");
                }

                var isCreated = await _orderServices.CreateOrderAsync(request);

                if (!isCreated)
                {
                    return BadRequest("Failed to create the order");
                }

                return Ok("Order created successfully");
            }
            catch (Exception ex)
            {
                _logger.Error("An error occurred while creating the order", ex);
                return InternalServerError(ex);
            }
        }

        [HttpDelete]
        [Route("{orderId}/items/{productId}")]
        public async Task<IHttpActionResult> RemoveOrderItem(string orderId, string productId)
        {

            try
            {

                if (string.IsNullOrEmpty(orderId) || string.IsNullOrEmpty(productId))
                {
                    _logger.Error("Order ID and Product ID are required.");
                    return BadRequest("Order ID and Product ID are required.");
                }

                var isRemoved = await _orderServices.RemoveOrderItemAsync(orderId, productId);

                if (!isRemoved)
                {
                    return NotFound();
                }

                return Ok("Order item removed successfully.");
            }
            catch (Exception ex)
            {
                _logger.Error("An error occurred while removing the order item", ex);
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("search")]
        public async Task<IHttpActionResult> SearchOrdersByKeyword([FromUri] string keyword)
        {

            try
            {
                if (string.IsNullOrEmpty(keyword))
                {
                    _logger.Error("Keyword is required.");
                    return BadRequest("Keyword is required.");
                }

                var orders = await _orderServices.SearchOrdersByKeywordAsync(keyword);

                if (orders == null || orders.Count == 0)
                {
                    return NotFound();
                }

                return Ok(orders);
            }
            catch (Exception ex)
            {
                _logger.Error("An error occurred while searching orders by product name", ex);
                return InternalServerError(ex);
            }
        }

    }
}
