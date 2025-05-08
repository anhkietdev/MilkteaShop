using DAL.Models;
using System;

namespace BAL.Dtos
{
    public class ComboItemProductSizeDto
    {
        public Guid? ProductSizeId { get; set; }
        public int Quantity { get; set; }
        public Size Size { get; set; }
        public decimal Price { get; set; }
    }
}