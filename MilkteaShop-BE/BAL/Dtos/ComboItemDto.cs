using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Dtos
{
    
        public class ComboItemDto
        {
            public Guid Id { get; set; }
            public string ComboCode { get; set; } = string.Empty;
            public string? Description { get; set; }
            public Guid ProductId { get; set; }
            public string ProductName { get; set; } = string.Empty;
            public decimal Price { get; set; }  // Added price
            public int Quantity { get; set; }
        }
    }


