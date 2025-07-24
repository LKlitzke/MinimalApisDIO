using Carter;
using Microsoft.AspNetCore.Mvc;
using MinimalApisDIO.Domain.Dtos;
using MinimalApisDIO.Domain.Interfaces;

namespace MinimalApisDIO.Endpoints
{
    public class AuthEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/v1/auth").WithTags("Authentication");

            group.MapPost("/login", async (LoginDto loginDto, [FromServices] IAdminServices adminServices, [FromServices] IJwtServices jwtServices) =>
            {
                var login = adminServices.Login(loginDto);
                if (login != null)
                {
                    var token = jwtServices.GenerateJwtToken(login);
                    return Results.Ok(new { Message = "Login successful!", Token = token });
                }
                else
                    return Results.Unauthorized();
            });

            group.MapPost("/create", async (AdminDto adminDto, IAdminServices adminServices) =>
            {
                if (adminDto.Email != null)
                {
                    return Results.BadRequest("Email is required.");
                }
                if (adminDto.Password != null)
                {
                    return Results.BadRequest("Password is required.");
                }
                if (adminDto.Profile != null)
                {
                    return Results.BadRequest("Profile is required.");
                }

                adminServices.Create(adminDto);
                return Results.Created();
            }).RequireAuthorization();

            group.MapGet("/list", async (IAdminServices adminServices) =>
            {
                var admins = adminServices.ListAll();
                return Results.Ok(admins);
            }).RequireAuthorization();
        }
    }
}
