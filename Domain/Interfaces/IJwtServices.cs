using MinimalApisDIO.Domain.Entities;

namespace MinimalApisDIO.Domain.Interfaces
{
    public interface IJwtServices
    {
        string GenerateJwtToken(Admin admin);
    }
}
