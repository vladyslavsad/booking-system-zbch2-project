using bookingSystemZBC.Domain.Entities;
using bookingSystemZBC.DTOs.Members;
using bookingSystemZBC.DTOs.Users;
using bookingSystemZBC.Repositories;

namespace bookingSystemZBC.Services
{
    public class MemberService(
        IMemberRepository memberRepository,
        IUserRepository userRepository) : IMemberService
    {
        public async Task<IReadOnlyList<MemberDTO>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var members = await memberRepository.GetAllAsync(cancellationToken);
            return members.Select(MapToDto).ToList();
        }

        public async Task<MemberDTO?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var member = await memberRepository.GetByIdAsync(id, cancellationToken);
            return member is not null ? MapToDto(member) : null;
        }

        public async Task<MemberDTO> CreateAsync(MemberCreateDTO request, CancellationToken cancellationToken = default)
        {
            var name = request.Name.Trim();
            var email = request.Email.Trim();

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new InvalidOperationException("Member name is required.");
            }

            if (string.IsNullOrWhiteSpace(email))
            {
                throw new InvalidOperationException("Member email is required.");
            }

            await EnsureEmailAvailableAsync(email, cancellationToken);
            var member = new Member
            {
                Name = name,
                Email = email,
                Age = request.Age
            };

            var memberCreated = await memberRepository.AddAsync(member, cancellationToken);
            await memberRepository.SaveChangesAsync(cancellationToken);

            var user = await userRepository.GetByEmailAsync(email, cancellationToken);
            if(user is not null)
            {
                user.MemberId = memberCreated.Id;
                userRepository.Update(user);
                await userRepository.SaveChangesAsync(cancellationToken);
            }


            return MapToDto(member);
        }

        public async Task<bool> Delete(int id, CancellationToken cancellationToken = default)
        {
            var member = await memberRepository.GetByIdAsync(id, cancellationToken);

            if (member is null)
            {
                return false;
            }

            memberRepository.Delete(member);
            await memberRepository.SaveChangesAsync(cancellationToken);
            return true;
        }

        private async Task EnsureEmailAvailableAsync(string email, CancellationToken cancellationToken)
        {
            var emailExists = await memberRepository.EmailExistsAsync(email, cancellationToken);

            if (emailExists)
            {
                throw new InvalidOperationException($"An account with email '{email}' already exists.");
            }
        }

        private static MemberDTO MapToDto(Member member) => new()
        {
            Id = member.Id,
            Name = member.Name,
            Email = member.Email,
            Age = member.Age
        };
    }
}
