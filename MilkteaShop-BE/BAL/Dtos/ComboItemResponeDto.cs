using System;
using System.Collections.Generic;

namespace BAL.Dtos
{
    public class ComboItemResponeDto
    {
        public Guid Id { get; set; }
        public string ComboCode { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool IsActive { get; set; }
        public List<ComboProductResponseDto> Products { get; set; } = new List<ComboProductResponseDto>();
    }
}