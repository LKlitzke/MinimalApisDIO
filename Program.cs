using Carter;
using Microsoft.EntityFrameworkCore;
using MinimalApisDIO.Domain.Interfaces;
using MinimalApisDIO.Domain.Services;
using MinimalApisDIO.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IAdminServices, AdminServices>();

builder.Services.AddDbContext<MySqlDbContext>(options =>
{
    var connection = builder.Configuration.GetConnectionString("DefaultConnection");

    options.UseMySql(
        connection,
        serverVersion: ServerVersion.AutoDetect(connection));
});

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCarter();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapCarter();

app.Run();
