using BAL.Dtos;

namespace BAL.Services.Interface
{
    public interface IVoucherService
    {
        Task<ICollection<VoucherResponseDto>> GetAllAsync();
        Task<VoucherResponseDto> GetByIdAsync(Guid id);
        Task CreateAsync(VoucherRequestDto voucherDto);
        Task UpdateAsync(Guid id, VoucherRequestDto voucherDto);
        Task<bool> DeleteAsync(Guid id);
    }
}
