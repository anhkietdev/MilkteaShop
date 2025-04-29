using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Store : BaseEntity
    {
        public required string StoreName { get; set; }

        // Mối quan hệ với User
        public Guid UserId { get; set; }
        public required virtual User User { get; set; }

        // Mối quan hệ với Order
        public Guid OrderId { get; set; }
        public required virtual Order Order { get; set; }
    }

}
