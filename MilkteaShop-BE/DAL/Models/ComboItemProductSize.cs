using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class ComboItemProductSize : BaseEntity
    {

        public Guid ComboItemId { get; set; }
        public ComboItem ComboItem { get; set; }

        public Guid ProductSizeId { get; set; }
        public ProductSize ProductSize { get; set; }
        public int Quantity { get; set; } 

    }
}
