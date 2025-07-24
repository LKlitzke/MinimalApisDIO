using MinimalApisDIO.Domain.Enums;

namespace MinimalApisDIO.Domain.Dtos
{
    public record AdminDto (string Email, string Password, Profile Profile);
    public record AdminResponseDto(int Id, string Email, string Profile);
}
