using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Dtos
{
    public class ComboItemResponeDto
    {
        public Guid Id { get; set; }
        public string ComboCode { get; set; } // Required field
        public string? Description { get; set; } // Optional field
        public List<ProductSizeResponseDto> ProductSizes { get; set; } = new();
        public decimal Price { get; set; }
    }
}
