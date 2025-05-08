using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Dtos
{
    public class ComboProductDto
    {
        public Guid? ProductId { get; set; }
        public string ProductName { get; set; }
        public string? Description { get; set; }
        public Guid CategoryId { get; set; }
        public string? ImageUrl { get; set; }
        public bool IsActive { get; set; }
        public string? ProductType { get; set; } = "Combo";
        public ComboItemProductSizeDto ProductSize { get; set; } = new ComboItemProductSizeDto();
    }
}
