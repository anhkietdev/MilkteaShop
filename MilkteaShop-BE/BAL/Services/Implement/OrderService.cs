using AutoMapper;
using BAL.Services.Interface;
using DAL.Models;
using DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
