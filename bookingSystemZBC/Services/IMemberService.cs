using bookingSystemZBC.DTOs.Members;

namespace bookingSystemZBC.Services
{
    public interface IMemberService
    {
        Task<IReadOnlyList<MemberDTO>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<MemberDTO?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<MemberDTO> CreateAsync(MemberCreateDTO request, CancellationToken cancellationToken = default);
        Task<bool> Delete(int id, CancellationToken cancellationToken = default);
    }
}
