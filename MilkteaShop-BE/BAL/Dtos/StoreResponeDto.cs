using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Dtos
{
    public class StoreResponeDto
    {

        public Guid Id { get; set; }
        public string StoreName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public bool IsActive { get; set; }

        public ICollection<OrderStoreResponseDto>? Orders { get; set; }
        public ICollection<UserDto>? Users { get; set; }

        public List<Guid>? UserIds { get; set; } = new();
        public List<Guid>? OrderIds { get; set; } = new();
        public decimal CashBalance { get; set; }
    }
}
