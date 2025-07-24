using Microsoft.EntityFrameworkCore;
using MinimalApisDIO.Domain.Entities;

namespace MinimalApisDIO.Infrastructure.Data
{
    public class MySqlDbContext : DbContext 
    {
        private readonly IConfiguration _configuration;

        public MySqlDbContext(DbContextOptions<MySqlDbContext> options, IConfiguration configuration) : base(options) 
        { 
            _configuration = configuration;
        }


        public DbSet<Admin> Admins { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connection = _configuration.GetConnectionString("DefaultConnection")?.ToString();

            if (!string.IsNullOrEmpty(connection)){
                optionsBuilder.UseMySql(connection, serverVersion: ServerVersion.AutoDetect(connection));
            }

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admin>().HasData(
                new Admin
                {
                    Id = 1,
                    Email = "admin@teste.com",
                    Password = "123456",
                    Profile = Domain.Enums.Profile.Admin
                }
            );
        }
    }
}
