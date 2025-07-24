using MinimalApisDIO.Domain.Entities;

namespace MinimalApisDIO.Domain.Interfaces
{
    public interface IVehicleServices
    {
        List<Vehicle> ListAll(int page = 1, string? name = null, string? brand = null);
        Vehicle? GetById(int id);
        Task Create(Vehicle vehicle);
        Task Update(Vehicle vehicle);
        Task Delete(Vehicle vehicle);
    }
}
