using Carter;
using MinimalApisDIO.Domain.Dtos;

namespace MinimalApisDIO.Endpoints
{
    public class AuthEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/login", async (LoginDto loginDto) =>
            {
                if (loginDto.Email == "admin@teste.com" && loginDto.Password == "123456")
                {
                    return Results.Ok("Login successful!");
                }
                else
                    return Results.Unauthorized();
            });

        }

        
    }
}
