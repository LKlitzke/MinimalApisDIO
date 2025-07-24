using Carter;
using MinimalApisDIO.Domain.Dtos;
using MinimalApisDIO.Domain.Entities;
using MinimalApisDIO.Domain.Interfaces;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace MinimalApisDIO.Endpoints
{
    public class VehicleEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/v1/vehicles").WithTags("Vehicles");

            group.MapGet("", async ([FromQuery] int? page, string? name, string? brand, [FromServices] IVehicleServices vehicleServices) =>
            {
                var vehicles = vehicleServices.ListAll(page, name, brand);
                return Results.Ok(vehicles);
            }).RequireAuthorization();

            group.MapGet("/{id:int}", async (int id, [FromServices] IVehicleServices vehicleServices) =>
            {
                var vehicle = vehicleServices.GetById(id);
                if (vehicle == null)
                {
                    return Results.NotFound();
                }
                return Results.Ok(vehicle);
            }).RequireAuthorization();

            group.MapPost("", async (VehicleDto vehicleDto, [FromServices] IVehicleServices vehicleServices) =>
            {
                var errorMessages = ValidateVehicleDto(vehicleDto);
                if (errorMessages.Any())
                {
                    return Results.BadRequest(new { Errors = errorMessages });
                }

                Vehicle vehicle = vehicleDto.Adapt<Vehicle>();

                await vehicleServices.Create(vehicle);
                return Results.Created($"/vehicles/{vehicle.Id}", vehicle);
            }).RequireAuthorization();

            group.MapPut("/{id:int}", async (int id, VehicleDto vehicleDto, [FromServices] IVehicleServices vehicleServices) =>
            {
                var errorMessages = ValidateVehicleDto(vehicleDto);
                if (errorMessages.Any())
                {
                    return Results.BadRequest(new { Errors = errorMessages });
                }

                Vehicle vehicle = vehicleDto.Adapt<Vehicle>();
                vehicle.Id = id;

                await vehicleServices.Update(vehicle);
                return Results.NoContent();
            }).RequireAuthorization();

            group.MapDelete("/{id:int}", async (int id, [FromServices] IVehicleServices vehicleServices) =>
            {
                var vehicle = vehicleServices.GetById(id);
                if (vehicle == null)
                {
                    return Results.NotFound();
                }
                await vehicleServices.Delete(vehicle);
                return Results.NoContent();
            }).RequireAuthorization().RequireAuthorization(new AuthorizeAttribute { Roles = "Admin" }); 
        }

        public List<string> ValidateVehicleDto(VehicleDto vehicleDto)
        {
            List<string> errorMessages = new();
            
            if(vehicleDto.Name == null)
            {
                errorMessages.Add("Vehicle name is required.");
            }
            if (vehicleDto.Brand == null)
            {
                errorMessages.Add("Vehicle brand is required.");
            }
            if (vehicleDto.Year < 1950)
            {
                errorMessages.Add("Vehicle year is too old. Only vehicles manufactured after 1950 are accepted.");
            }
            return errorMessages;
        }
    }
}