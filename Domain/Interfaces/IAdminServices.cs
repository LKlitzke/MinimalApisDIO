using MinimalApisDIO.Domain.Dtos;
using MinimalApisDIO.Domain.Entities;

namespace MinimalApisDIO.Domain.Interfaces
{
    public interface IAdminServices
    {
        Admin Login(LoginDto loginDto);
        Task Create(AdminDto adminDto);
        List<AdminResponseDto> ListAll();
    }
}
