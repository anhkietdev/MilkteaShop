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


            public Guid Id { get; set; } // Từ BaseEntity
            public string StoreName { get; set; } = string.Empty;
            public Guid UserId { get; set; }
            public Guid OrderId { get; set; }
        


    }
}
