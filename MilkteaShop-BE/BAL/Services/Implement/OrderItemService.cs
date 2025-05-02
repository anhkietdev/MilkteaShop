using AutoMapper;
using BAL.Dtos;
using BAL.Services.Interface;
using DAL.Models;
using DAL.Repositories.Interfaces;

namespace BAL.Services.Implement
{
    public class OrderItemService : IOrderItemService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IOrderService _orderService;

        public OrderItemService(IUnitOfWork unitOfWork, IMapper mapper, IOrderService orderService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _orderService = orderService;
        }

        public async Task CreateOrderItemAsync(OrderItemRequestDto orderItemDto)
        {            
            var orderItem = _mapper.Map<OrderItem>(orderItemDto);
            
            var productSize = await _unitOfWork.ProductSize.GetAsync(ps => ps.Id == orderItemDto.ProductSizeId);
            if (productSize == null)
            {
                throw new Exception("ProductSize not found");
            }
            
            var basePrice = productSize.Price * orderItemDto.Quantity;
            
            decimal toppingTotal = 0;
            if (orderItem.Toppings != null)
            {
                foreach (var topping in orderItem.Toppings)
                {
                    toppingTotal += topping.ProductSize.Price;
                }
            }
           
            orderItem.Price = basePrice + toppingTotal;
            
            var order = await _unitOfWork.Orders.GetAsync(o => o.Id == orderItemDto.OrderId);
            if (order == null)
            {
                throw new Exception("Order not found");
            }

            order.TotalAmount += orderItem.Price;
            
            await _unitOfWork.Orders.UpdateAsync(order);
            await _unitOfWork.OrderItems.AddAsync(orderItem);
            await _unitOfWork.SaveAsync();
        }


        public Task<bool> DeleteOrderItemAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<OrderItem>> GetAllOrderItemsAsync()
        {
            ICollection<OrderItem> orderItems = await _unitOfWork.OrderItems.GetAllAsync();
            if (orderItems == null)
            {
                throw new Exception("No order items found");
            }
            return orderItems;
        }

        public async Task<OrderItem> GetOrderItemByIdAsync(Guid id)
        {
            OrderItem? orderItem = await _unitOfWork.OrderItems.GetAsync(o => o.Id == id);
            if (orderItem == null)
            {
                throw new Exception("Order item not found");
            }
            return orderItem;
        }

        public async Task UpdateOrderItemAsync(Guid id, OrderItemRequestDto orderItemDto)
        {
            OrderItem? orderItem = await _unitOfWork.OrderItems.GetAsync(o => o.Id == id);
            if (orderItem == null)
            {
                throw new Exception("Order item not found");
            }
            _mapper.Map(orderItemDto, orderItem);

            orderItem.Quantity = orderItemDto.Quantity;
            orderItem.ProductSizeId = orderItemDto.ProductSizeId;
            orderItem.OrderId = orderItemDto.OrderId;

            await _unitOfWork.OrderItems.UpdateAsync(orderItem);
            await _unitOfWork.SaveAsync();
        }
    }
}
