using ApiServiceTest.EntityRequests;
using ApiServiceTest.OrderDTO;
using ApiServiceTest.Models;
using ApiServiceTest.Services;
using ApiServiceTest.UnitOfWorks;
using log4net;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace ApiServiceTest.Controllers
{
    [RoutePrefix("api/v1")]
    public class OrdersController : ApiController
    {
        private readonly OrderServices _orderServices;
        private readonly IUnitOfWork _unitOfWork;
        private static readonly ILog _logger = LogManager.GetLogger(typeof(OrdersController));

        public OrdersController(IUnitOfWork unitOfWork, OrderServices orderServices)
        {
            _unitOfWork = unitOfWork;
            _orderServices = orderServices;
        }

        // Lấy danh sách đơn hàng
        [HttpGet]
        [Route("orders")]
        public async Task<IHttpActionResult> GetOrders()
        {
            try
            {
                var orders = await _orderServices.GetOrdersAsync();

                if (orders == null || !orders.Any())
                {
                    return NotFound();
                }

                var orderResponses = OrderMapper.ToOrderResponseList(orders);
                return Ok(orderResponses);
            }
            catch (Exception ex)
            {
                _logger.Error("Có lỗi khi lấy danh sách đơn hàng", ex);
                return InternalServerError(ex);
            }
        }

        // Lấy chi tiết đơn hàng
        [HttpGet]
        [Route("orders/{orderId}")]
        public async Task<IHttpActionResult> GetOrderDetails(string orderId)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(orderId))
                {
                    return BadRequest("Order ID không được để trống.");
                }

                var order = await _orderServices.GetOrderDetailsAsync(orderId);

                if (order == null)
                {
                    return NotFound();
                }

                var orderDetailResponse = OrderMapper.ToOrderDetailResponse(order);
                return Ok(orderDetailResponse);
            }
            catch (Exception ex)
            {
                _logger.Error("Có lỗi khi lấy chi tiết đơn hàng", ex);
                return InternalServerError(ex);
            }
        }

        // Tạo đơn hàng mới
        [HttpPost]
        [Route("orders")]
        public async Task<IHttpActionResult> CreateOrder([FromBody] CreateOrderRequest request)
        {
            try
            {
                if (request == null || request.OrderItems == null || !request.OrderItems.Any())
                {
                    return BadRequest("Đơn hàng và danh sách mặt hàng không được để trống.");
                }

                if (request.OrderItems.Any(item => item.Quantity <= 0))
                {
                    return BadRequest("Số lượng của mỗi mặt hàng phải lớn hơn 0.");
                }

                var orderItems = request.OrderItems.Select(item => new OrderItem
                {
                    ProductID = item.ProductID,
                    Quantity = item.Quantity,
                }).ToList();

                decimal totalAmount = 0;
                foreach (var orderItem in orderItems)
                {
                    if (string.IsNullOrWhiteSpace(orderItem.ProductID))
                    {
                        return BadRequest("Product ID không được để trống.");
                    }

                    var product = await _unitOfWork.ProductRepository.GetProductByIdAsync(orderItem.ProductID);
                    if (product == null)
                    {
                        return BadRequest($"Không tìm thấy sản phẩm với ID: {orderItem.ProductID}.");
                    }

                    orderItem.Price = product.Price;
                    totalAmount += (orderItem.Quantity ?? 0) * (orderItem.Price ?? 0);
                }

                var order = new Order
                {
                    OrderID = Guid.NewGuid().ToString(),
                    OrderDate = DateTime.Now,
                    CustomerID = request.CustomerID,
                    ShippingProviderID = request.ShippingProviderID,
                    PaymentMethodID = request.PaymentMethodID,
                    TotalAmount = totalAmount,
                    DeliveryStatus = request.DeliveryStatus,
                    OverdueDate = request.OverdueDate,
                    PaymentStatus = request.PaymentStatus,
                    PaidAt = request.PaidAt,
                    OrderItems = orderItems
                };

                var isCreated = await _orderServices.CreateOrderAsync(order);

                if (!isCreated)
                {
                    return BadRequest("Không thể tạo đơn hàng.");
                }

                return Ok("Đơn hàng được tạo thành công.");
            }
            catch (Exception ex)
            {
                _logger.Error("Có lỗi khi tạo đơn hàng", ex);
                return InternalServerError(ex);
            }
        }

        // Xóa item khỏi đơn hàng
        [HttpDelete]
        [Route("orders/{orderId}/items/{productId}")]
        public async Task<IHttpActionResult> RemoveOrderItem(string orderId, string productId)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(orderId) || string.IsNullOrWhiteSpace(productId))
                {
                    return BadRequest("Order ID và Product ID không được để trống.");
                }

                var isRemoved = await _orderServices.RemoveOrderItemAsync(orderId, productId);

                if (!isRemoved)
                {
                    return NotFound();
                }

                return Ok("Mặt hàng đã được xóa khỏi đơn hàng thành công.");
            }
            catch (Exception ex)
            {
                _logger.Error("Có lỗi khi xóa mặt hàng khỏi đơn hàng", ex);
                return InternalServerError(ex);
            }
        }

        // Tìm kiếm đơn hàng theo keyword
        [HttpGet]
        [Route("orders/search/keyword/{keyword}")]
        public async Task<IHttpActionResult> SearchOrdersByKeyword(string keyword)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(keyword))
                {
                    return BadRequest("Từ khóa không được để trống.");
                }

                var orders = await _orderServices.SearchOrdersByKeywordAsync(keyword);

                if (orders == null || !orders.Any())
                {
                    return NotFound();
                }

                var orderResponses = OrderMapper.ToOrderResponseList(orders);
                return Ok(orderResponses);
            }
            catch (Exception ex)
            {
                _logger.Error("Có lỗi khi tìm kiếm đơn hàng", ex);
                return InternalServerError(ex);
            }
        }
    }
}
