using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Dtos
{
    public class CategoryExtraMappingDto
    {
        public Guid Id { get; set; }
        public Guid MainCategoryId { get; set; }
        public Guid ExtraCategoryId { get; set; }

        // Optional: Include names or other info if needed
        // public string MainCategoryName { get; set; }
        // public string ExtraCategoryName { get; set; }
    }

}
