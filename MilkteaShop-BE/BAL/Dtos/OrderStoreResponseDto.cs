using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Dtos
{
    public class OrderStoreResponseDto
    {
        public Guid Id { get; set; }
        public required string OrderNumber { get; set; }
        public decimal TotalAmount { get; set; }
        public string? Description { get; set; }
        public ICollection<OrderItemResponseDto>? OrderItems { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
    }
}
