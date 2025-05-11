using AutoMapper;
using BAL.Dtos;
using BAL.Services.Interface;
using DAL.Models;
using DAL.Repositories.Interfaces;

namespace BAL.Services.Implement
{
    public class VoucherService : IVoucherService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public VoucherService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ICollection<VoucherResponseDto>> GetAllAsync()
        {
            var includeProperties = "OrderList";
            var vouchers = await _unitOfWork.Vouchers.GetAllAsync(null,includeProperties);
            if (vouchers == null || !vouchers.Any())
            {
                throw new Exception("No vouchers found");
            }

            return _mapper.Map<ICollection<VoucherResponseDto>>(vouchers);
        }
        public async Task<VoucherResponseDto> GetByIdAsync(Guid id)
        {
            var includeProperties = "OrderList";
            var voucher = await _unitOfWork.Vouchers.GetAsync(v => v.Id == id, includeProperties);
            return _mapper.Map<VoucherResponseDto>(voucher);
        }
        public async Task CreateAsync(VoucherRequestDto voucherDto)
        {
            var voucher = _mapper.Map<Voucher>(voucherDto);

            voucher.VoucherCode = Voucher.GenerateVoucherNumber();
            voucher.ExceedDate = DateTime.UtcNow.AddDays(5);

            await _unitOfWork.Vouchers.AddAsync(voucher);
            await _unitOfWork.SaveAsync();
        }
        public async Task UpdateAsync(Guid id, VoucherRequestDto voucherDto)
        {
            var voucher = await _unitOfWork.Vouchers.GetAsync(v => v.Id == id);
            if (voucher == null) throw new Exception("Voucher not found");

            _mapper.Map(voucherDto, voucher);

            voucher.VoucherCode = voucherDto.VoucherCode;
            voucher.PriceCondition = voucherDto.PriceCondition;
            voucher.DiscountPercentage = voucherDto.DiscountPercentage;
            voucher.IsActive = voucherDto.IsActive;
            voucher.ExceedDate = voucherDto.ExceedDate;

            await _unitOfWork.Vouchers.UpdateAsync(voucher);

            await _unitOfWork.SaveAsync();
        }
        public async Task<bool> DeleteAsync(Guid id)
        {
            var voucher = await _unitOfWork.Vouchers.GetAsync(v => v.Id == id);
            if (voucher == null) return false;
            await _unitOfWork.Vouchers.RemoveAsync(voucher);
            await _unitOfWork.SaveAsync();
            return true;
        }
        public async Task<RequiredOrderDto> GetOrderRelatedAmount(Guid orderId)
        {
            var order = await _unitOfWork.Orders.GetAsync(o => o.Id == orderId);
            if (order == null)
            {
                throw new Exception("Order not found");
            }
            var voucher = await _unitOfWork.Vouchers.GetAsync(v => v.Id == order.VoucherId);
            if (voucher == null)
            {
                throw new Exception("Voucher not found");
            }
            return new RequiredOrderDto
            {
                OrderId = order.Id,
                TotalPrice = order.TotalAmount / (voucher.DiscountPercentage / 100),
                VoucherId = voucher.Id,
                PercentageDiscount = voucher.DiscountPercentage,
                AmountDiscount = order.TotalAmount
            };
        }
    }
}
