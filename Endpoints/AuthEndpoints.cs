using Carter;
using MinimalApisDIO.Domain.Dtos;
using MinimalApisDIO.Domain.Interfaces;

namespace MinimalApisDIO.Endpoints
{
    public class AuthEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/login", async (LoginDto loginDto, IAdminServices adminServices) =>
            {
                if (adminServices.Login(loginDto) != null)
                {
                    return Results.Ok("Login successful!");
                }
                else
                    return Results.Unauthorized();
            });
        }
    }
}
