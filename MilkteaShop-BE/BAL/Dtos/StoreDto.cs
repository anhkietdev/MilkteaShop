using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Dtos
{
    public class StoreDto
    {

        public string StoreName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        

        // Optionally, you can include IDs or minimal info about Users and Orders,
        // but usually DTOs keep it simple unless you need nested data.
        public List<int>? UserIds { get; set; }
        public List<int>? OrderIds { get; set; }


    }
}
