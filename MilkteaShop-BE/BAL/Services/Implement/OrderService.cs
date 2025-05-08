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
        private readonly IVoucherService _voucherService;
        private readonly IMapper _mapper;

        public OrderService(IUnitOfWork unitOfWork, IVoucherService voucherService, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _voucherService = voucherService;
            _mapper = mapper;
        }

        public async Task<OrderResponseDto> CreateOrderAsync(OrderRequestDto orderRequest)
        {
            string orderNumber = Order.GenerateOrderNumber();
            Order newOrder = _mapper.Map<Order>(orderRequest);

            newOrder.OrderNumber = orderNumber;
            newOrder.CreatedAt = DateTime.UtcNow;
            newOrder.PaymentMethod = orderRequest.PaymentMethod;
            newOrder.StoreId = orderRequest.StoreId;

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
            string includeProperties = "OrderItems,OrderItems.ProductSize,OrderItems.Toppings";
            ICollection<Order> orders = await _unitOfWork.Orders.GetAllAsync(null,includeProperties);
            if (orders == null)
            {
                throw new Exception("No order found");
            }

            return _mapper.Map<ICollection<OrderResponseDto>>(orders);
        }

        public async Task<OrderResponseDto> GetOrderByIdAsync(Guid id)
        {
            string includeProperties = "OrderItems,OrderItems.ProductSize,OrderItems.Toppings";
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

                    existingOrder.OrderItems.Add(orderItem);
                }
            }
            await _unitOfWork.SaveAsync();
            OrderResponseDto orderResponse = _mapper.Map<OrderResponseDto>(existingOrder);
            return orderResponse;
        }

        public async Task<OrderSummaryDto> GetTodayOrdersAsync(Guid? storeId = null)
        {
            DateTime today = DateTime.UtcNow.Date;
            DateTime tomorrow = today.AddDays(1);

            var orders = await GetOrdersInTimeRange(today, tomorrow, storeId);

            string storeName = string.Empty;
            if (storeId.HasValue)
            {
                var store = await _unitOfWork.Stores.GetAsync(s => s.Id == storeId.Value);
                storeName = store?.StoreName ?? "Unknown Store";
            }

            return new OrderSummaryDto
            {
                TotalOrders = orders.Count,
                TotalAmount = orders.Sum(o => o.TotalAmount),
                StartDate = today,
                EndDate = tomorrow,
                StoreId = storeId,
                StoreName = storeName
            };
        }

        public async Task<OrderSummaryDto> GetWeekOrdersAsync(Guid? storeId = null)
        {
            DateTime today = DateTime.UtcNow.Date;
            int diff = (7 + (today.DayOfWeek - DayOfWeek.Monday)) % 7;
            DateTime startOfWeek = today.AddDays(-diff);
            DateTime endOfWeek = startOfWeek.AddDays(7);

            var orders = await GetOrdersInTimeRange(startOfWeek, endOfWeek, storeId);

            string storeName = string.Empty;
            if (storeId.HasValue)
            {
                var store = await _unitOfWork.Stores.GetAsync(s => s.Id == storeId.Value);
                storeName = store?.StoreName ?? "Unknown Store";
            }

            return new OrderSummaryDto
            {
                TotalOrders = orders.Count,
                TotalAmount = orders.Sum(o => o.TotalAmount),
                StartDate = startOfWeek,
                EndDate = endOfWeek,
                StoreId = storeId,
                StoreName = storeName
            };
        }

        public async Task<OrderSummaryDto> GetMonthOrdersAsync(Guid? storeId = null)
        {
            DateTime today = DateTime.UtcNow.Date;
            DateTime startOfMonth = new DateTime(today.Year, today.Month, 1);
            DateTime endOfMonth = startOfMonth.AddMonths(1);

            var orders = await GetOrdersInTimeRange(startOfMonth, endOfMonth, storeId);

            string storeName = string.Empty;
            if (storeId.HasValue)
            {
                var store = await _unitOfWork.Stores.GetAsync(s => s.Id == storeId.Value);
                storeName = store?.StoreName ?? "Unknown Store";
            }

            return new OrderSummaryDto
            {
                TotalOrders = orders.Count,
                TotalAmount = orders.Sum(o => o.TotalAmount),
                StartDate = startOfMonth,
                EndDate = endOfMonth,
                StoreId = storeId,
                StoreName = storeName
            };
        }

        public async Task<OrderSummaryDto> GetYearOrdersAsync(Guid? storeId = null)
        {
            DateTime today = DateTime.UtcNow.Date;
            DateTime startOfYear = new DateTime(today.Year, 1, 1);
            DateTime endOfYear = startOfYear.AddYears(1);

            var orders = await GetOrdersInTimeRange(startOfYear, endOfYear, storeId);

            string storeName = string.Empty;
            if (storeId.HasValue)
            {
                var store = await _unitOfWork.Stores.GetAsync(s => s.Id == storeId.Value);
                storeName = store?.StoreName ?? "Unknown Store";
            }

            return new OrderSummaryDto
            {
                TotalOrders = orders.Count,
                TotalAmount = orders.Sum(o => o.TotalAmount),
                StartDate = startOfYear,
                EndDate = endOfYear,
                StoreId = storeId,
                StoreName = storeName
            };
        }

        public async Task<ICollection<OrderSummaryDto>> GetOrdersByStoreAsync(Guid storeId)
        {
            var store = await _unitOfWork.Stores.GetAsync(s => s.Id == storeId);
            if (store == null)
            {
                throw new Exception($"Store with ID {storeId} not found");
            }

            var todaySummary = await GetTodayOrdersAsync(storeId);
            var weekSummary = await GetWeekOrdersAsync(storeId);
            var monthSummary = await GetMonthOrdersAsync(storeId);
            var yearSummary = await GetYearOrdersAsync(storeId);

            return new List<OrderSummaryDto> { todaySummary, weekSummary, monthSummary, yearSummary };
        }

        private async Task<ICollection<Order>> GetOrdersInTimeRange(DateTime startDate, DateTime endDate, Guid? storeId = null)
        {
            ICollection<Order> orders;

            if (storeId.HasValue)
            {
                orders = await _unitOfWork.Orders.GetAllAsync(
                    o => o.CreatedAt >= startDate && o.CreatedAt < endDate && o.StoreId == storeId.Value);
            }
            else
            {
                orders = await _unitOfWork.Orders.GetAllAsync(
                    o => o.CreatedAt >= startDate && o.CreatedAt < endDate);
            }

            return orders;
        }

        public async Task<ICollection<TopSellingProductDto>> GetTop5BestSellingProductsAsync()
        {
            string includeProperties = "OrderItems,OrderItems.ProductSize,OrderItems.ProductSize.Product";

            // Lấy tất cả các đơn hàng với các sản phẩm trong OrderItems
            var orders = await _unitOfWork.Orders.GetAllAsync(
                o => o.OrderItems.Any(), includeProperties);

            // Đếm tổng số lượng sản phẩm bán được
            var productSales = orders
                .SelectMany(order => order.OrderItems)
                .GroupBy(oi => oi.ProductSize.ProductId) // nhóm theo ProductId
                .Select(group => new
                {
                    ProductId = group.Key,
                    TotalQuantitySold = group.Sum(oi => oi.Quantity)
                })
                .OrderByDescending(product => product.TotalQuantitySold) // Sắp xếp theo số lượng bán
                .Take(5) // Lấy top 5
                .ToList();

            // Tạo danh sách DTO cho kết quả trả về
            var topSellingProducts = new List<TopSellingProductDto>();

            foreach (var productSale in productSales)
            {
                var product = await _unitOfWork.Products.GetAsync(p => p.Id == productSale.ProductId);

                if (product != null)
                {
                    topSellingProducts.Add(new TopSellingProductDto
                    {
                        ProductId = product.Id,
                        ProductName = product.ProductName,
                        ImageUrl = product.ImageUrl,
                        CategoryId = product.CategoryId,
                        TotalQuantitySold = productSale.TotalQuantitySold,

                    });
                }
            }

            return topSellingProducts;
        }

        public async Task<bool> ApplyVoucher(ApplyVoucherDto applyVoucherDto)
        {
            var order = await _unitOfWork.Orders.GetAsync(o => o.Id == applyVoucherDto.OrderId);
            if (order == null)
            {
                throw new Exception("Order not found");
            }
            var voucher = await _unitOfWork.Vouchers.GetAsync(v => v.Id == applyVoucherDto.VoucherId);
            if (voucher == null)
            {
                throw new Exception("Voucher not found");
            }
            var currentAmount = order.TotalAmount;
            if (voucher.IsActive && order.TotalAmount >= voucher.PriceCondition)
            {
                order.VoucherId = applyVoucherDto.VoucherId;
                order.TotalAmount = currentAmount - (order.TotalAmount * (voucher.DiscountPercentage/100));
                await _unitOfWork.Orders.UpdateAsync(order);

                voucher.IsActive = false;
                await _unitOfWork.Vouchers.UpdateAsync(voucher);

                await _unitOfWork.SaveAsync();

                return true;
            }
            else
            {
                throw new Exception("Voucher is not valid for this order");
            }
        }
        public async Task<OrderResponseDto> CreateOrderComboAsync(OrderComboRequest orderRequest)
        {
            var orderNumber = Order.GenerateOrderNumber();
            var newOrder = _mapper.Map<Order>(orderRequest);

            newOrder.OrderNumber = orderNumber;
            newOrder.CreatedAt = DateTime.UtcNow;
            newOrder.PaymentMethod = orderRequest.PaymentMethod;
            newOrder.StoreId = orderRequest.StoreId;
            newOrder.TotalAmount = 0;
            newOrder.OrderStatus = "Processing";
            newOrder.TotalAmount += await ProcessComboItemsAsync(orderRequest.ComboItems, newOrder);

            await _unitOfWork.Orders.AddAsync(newOrder);
            await _unitOfWork.SaveAsync();

            return _mapper.Map<OrderResponseDto>(newOrder);
        }

        private async Task<decimal> ProcessComboItemsAsync(IEnumerable<Guid> comboItems, Order newOrder)
        {
            if (comboItems == null || !comboItems.Any()) return 0;

            decimal totalAmount = 0;

            var combos = await _unitOfWork.ComboItems.GetAllAsync(
                c => comboItems.Contains(c.Id),
                includeProperties: "ComboItemProductSizes.ProductSize"
            );

            foreach (var comboId in comboItems)
            {
                var combo = combos.FirstOrDefault(c => c.Id == comboId);

                if (combo == null)
                {
                    throw new ArgumentException($"Combo with ID {comboId} not found.");
                }

                foreach (var comboItem in combo.ComboItemProductSizes)
                {
                    var productSize = comboItem.ProductSize;
                    if (productSize == null)
                    {
                        throw new ArgumentException($"ProductSize with ID {comboItem.ProductSizeId} not found in Combo Item.");
                    }

                    var orderItem = new OrderItem
                    {
                        ProductSizeId = productSize.Id,
                        Quantity = comboItem.Quantity,
                        Price = productSize.Price,
                        Description = $"Combo: {combo.Description}",
                        Order = newOrder,
                    };

                    newOrder.OrderItems.Add(orderItem);
                    totalAmount += orderItem.Price * orderItem.Quantity;
                }
            }

            return totalAmount;
        }
    }
}
