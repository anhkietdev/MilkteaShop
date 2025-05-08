using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Dtos
{
    public class ComboProductSizeResponseDto
    {
        public Guid ProductSizeId { get; set; }
        public string Size { get; set; }
        public decimal Price { get; set; }
    }
}
