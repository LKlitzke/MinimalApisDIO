using MinimalApisDIO.Domain.Dtos;
using MinimalApisDIO.Domain.Entities;
using MinimalApisDIO.Domain.Interfaces;
using MinimalApisDIO.Infrastructure.Data;

namespace MinimalApisDIO.Domain.Services
{
    public class AdminServices : IAdminServices
    {
        private readonly MySqlDbContext _dbContext;

        public AdminServices(MySqlDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Admin Login(LoginDto loginDto)
        {
            var admin = _dbContext.Admins.FirstOrDefault(a => a.Email == loginDto.Email && a.Password == loginDto.Password);

            return admin;
        }
    }
}
