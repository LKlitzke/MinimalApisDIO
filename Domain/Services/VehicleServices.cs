using MinimalApisDIO.Domain.Entities;
using MinimalApisDIO.Domain.Interfaces;
using MinimalApisDIO.Infrastructure.Data;

namespace MinimalApisDIO.Domain.Services
{
    public class VehicleServices : IVehicleServices
    {
        private readonly MySqlDbContext _dbContext;

        public VehicleServices(MySqlDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task Create(Vehicle vehicle)
        {
            _dbContext.Vehicles.Add(vehicle);
            _dbContext.SaveChanges();

            return Task.CompletedTask;
        }

        public Task Update(Vehicle vehicle)
        {
            _dbContext.Vehicles.Update(vehicle);
            _dbContext.SaveChanges();

            return Task.CompletedTask;
        }

        public Task Delete(Vehicle vehicle)
        {
            _dbContext.Vehicles.Remove(vehicle);
            _dbContext.SaveChanges();

            return Task.CompletedTask;
        }

        public Vehicle? GetById(int id)
        {
            var vehicle = _dbContext.Vehicles.Find(id);
            if (vehicle == null)
            {
                return null;
            }
            else return vehicle;
        }

        public List<Vehicle> ListAll(int page = 1, string? name = null, string? brand = null)
        {
            var query = _dbContext.Vehicles.AsQueryable();

            if(!string.IsNullOrEmpty(name))
            {
                query = query.Where(v => v.Name.Contains(name, StringComparison.OrdinalIgnoreCase));
            }

            int pageSize = 10;
            query = query.Skip((page - 1) * pageSize).Take(pageSize);

            return query.ToList();
        }
    }
}
