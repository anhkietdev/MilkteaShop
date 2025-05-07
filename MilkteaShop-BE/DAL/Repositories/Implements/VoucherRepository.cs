using DAL.Context;
using DAL.Models;
using DAL.Repositories.Interfaces;

namespace DAL.Repositories.Implements
{
    public class VoucherRepository : Repository<Voucher>, IVoucherRepository
    {
        public VoucherRepository(AppDbContext context) : base(context)
        {
        }
    }
}
