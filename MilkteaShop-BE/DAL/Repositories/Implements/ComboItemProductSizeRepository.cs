using DAL.Context;
using DAL.Models;
using DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Implements
{
    public class ComboItemProductSizeRepository : Repository<ComboItemProductSize>, IComboItemProductSizeRepository
    {
        public ComboItemProductSizeRepository(AppDbContext context) : base(context)
        {
        }
    }
}
