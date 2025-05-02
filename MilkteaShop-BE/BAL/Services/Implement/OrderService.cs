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

        public async Task<bool> DeleteOrderAsync(Guid id)
        {
            Order? order = await _unitOfWork.Orders.GetAsync(o => o.Id == id);
            if (order == null)
            {
                throw new Exception("Order not found");
            }
            await _unitOfWork.Orders.RemoveAsync(order);
            await _unitOfWork.SaveAsync();
            return true;
        }

        public async Task<ICollection<OrderResponseDto>> GetAllAsync()
        {
            string includeProperties = "OrderItems,OrderItems.ProductSize,OrderItems.ToppingItems";
            ICollection<Order> orders = await _unitOfWork.Orders.GetAllAsync(null,includeProperties);
            if (orders == null)
            {
                throw new Exception("No order found");
            }

            return _mapper.Map<ICollection<OrderResponseDto>>(orders);
        }

        public async Task<OrderResponseDto> GetOrderByIdAsync(Guid id)
        {
            string includeProperties = "OrderItems,OrderItems.ProductSize,OrderItems.ToppingItems";
            Order? order = await _unitOfWork.Orders.GetAsync(o => o.Id == id, includeProperties);
            if (order == null)
            {
                throw new Exception("Order not found");
            }
            OrderResponseDto orderResponse = _mapper.Map<OrderResponseDto>(order);
            return orderResponse;
        }

        public async Task<OrderResponseDto> UpdateOrderAsync(Guid id, OrderRequestDto order)
        {
            Order? existingOrder = await _unitOfWork.Orders.GetAsync(o => o.Id == id);
            if (existingOrder == null)
            {
                throw new Exception("Order not found");
            }

            existingOrder.TotalAmount = order.TotalAmount;
            existingOrder.Description = order.Description;
            existingOrder.PaymentMethod = order.PaymentMethod;
            existingOrder.

            // Handle order items if they exist in the request
            if (order.OrderItems != null && order.OrderItems.Any())
            {
                existingOrder.OrderItems.Clear(); // Clear existing items
                foreach (var itemDto in order.OrderItems)
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
                        Order = existingOrder,
                    };
                    if (itemDto.ParentOrderItemId.HasValue)
                    {
                        orderItem.ParentOrderItemId = itemDto.ParentOrderItemId;
                    }
                    existingOrder.OrderItems.Add(orderItem);
                }
            }
            await _unitOfWork.SaveAsync();
            OrderResponseDto orderResponse = _mapper.Map<OrderResponseDto>(existingOrder);
            return orderResponse;
        }
    }
}
