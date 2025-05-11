using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL
{
   
        public class RequiredOrderDto
        {
            public Guid OrderId { get; set; }
            public decimal TotalPrice { get; set; }
            public Guid VoucherId { get; set; }
            public decimal PercentageDiscount { get; set; }
            public decimal AmountDiscount { get; set; }
        }

    
}
