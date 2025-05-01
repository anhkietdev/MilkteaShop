using AutoMapper;
using BAL.Dtos;
using BAL.Services.Interface;
using DAL.Models;
using DAL.Repositories.Interfaces;

namespace BAL.Services.Implement
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OrderService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<OrderResponseDto> CreateOrderAsync(OrderRequestDto orderRequest)
        {
            string orderNumber = Order.GenerateOrderNumber();
            Order newOrder = _mapper.Map<Order>(orderRequest);

            newOrder.OrderNumber = orderNumber;
            newOrder.CreatedAt = DateTime.UtcNow;
            newOrder.PaymentMethod = orderRequest.PaymentMethod;

            // Handle order items if they exist in the request
            if (orderRequest.OrderItems != null && orderRequest.OrderItems.Any())
            {
                decimal totalAmount = 0;

                foreach (var itemDto in orderRequest.OrderItems)
                {
                    var product = await _unitOfWork.ProductSize.GetAsync(p => p.Id == itemDto.ProductSizeId);
                    if (product == null)
                    {
                        throw new ArgumentException($"Product with ID {itemDto.ProductSizeId} not found.");
                    }

                    OrderItem orderItem = new OrderItem
                    {
                        ProductSizeId = itemDto.ProductSizeId,
                        ProductSize = product,
                        Quantity = itemDto.Quantity,
                        Price = product.Price,
                        Description = itemDto.Description,
                        Order = newOrder,
                    };

                    if (itemDto.ParentOrderItemId.HasValue)
                    {
                        orderItem.ParentOrderItemId = itemDto.ParentOrderItemId;
                    }

                    newOrder.OrderItems.Add(orderItem);

                    decimal itemTotal = orderItem.Price * orderItem.Quantity;
                    totalAmount += itemTotal;
                }

                newOrder.TotalAmount = totalAmount;
            }
            else
            {
                newOrder.TotalAmount = 0;
            }

            await _unitOfWork.Orders.AddAsync(newOrder);
            await _unitOfWork.SaveAsync();

            OrderResponseDto orderResponse = _mapper.Map<OrderResponseDto>(newOrder);
            return orderResponse;
        }

        public async Task<ICollection<Order>> GetAllAsync()
        {
            ICollection<Order> orders = await _unitOfWork.Orders.GetAllAsync();
            if (orders == null)
            {
                throw new Exception("No order found");
            }
            return orders;
        }

        public async Task<Order> GetOrderByIdAsync(Guid id)
        {
            Order? order = await _unitOfWork.Orders.GetAsync(c => c.Id == id);
            if (order == null)
            {
                throw new Exception("order not found");
            }
            return order;
        }
    }
}
