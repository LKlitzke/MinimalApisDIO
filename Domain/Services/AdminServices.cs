using Mapster;
using MinimalApisDIO.Domain.Dtos;
using MinimalApisDIO.Domain.Entities;
using MinimalApisDIO.Domain.Enums;
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

        public Task Create(AdminDto adminDto)
        {
            var admin = adminDto.Adapt<Admin>();
            _dbContext.Admins.Add(admin);
            _dbContext.SaveChanges();

            return Task.CompletedTask;
        }

        public List<AdminResponseDto> ListAll()
        {
            var adminList = _dbContext.Admins.ToList();
            var adminResponseList = adminList.Select(a => new AdminResponseDto
            (
                Id: a.Id,
                Email: a.Email,
                Profile: a.Profile.ToString()
            )).ToList();

            return adminResponseList;
        }

        public Admin Login(LoginDto loginDto)
        {
            var admin = _dbContext.Admins.FirstOrDefault(a => a.Email == loginDto.Email && a.Password == loginDto.Password);

            return admin;
        }

    }
}
