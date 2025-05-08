using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Dtos
{
    public class ComboProductResponseDto
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; } 
        public bool IsActive { get; set; }
        public ComboProductSizeResponseDto ProductSize { get; set; } = new ComboProductSizeResponseDto();
    }
}
