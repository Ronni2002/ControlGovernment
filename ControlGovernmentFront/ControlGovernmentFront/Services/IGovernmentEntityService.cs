using ControlGovernmentFront.Models;

namespace ControlGovernmentFront.Services
{
    public interface IGovernmentEntityService
    {
        Task<IEnumerable<GovernmentEntity>> GetAllAsync();
        Task<GovernmentEntity> GetByIdAsync(int id);
        Task CreateAsync(GovernmentEntity entity);
        Task UpdateAsync(GovernmentEntity entity);
        Task DeleteAsync(int id);
    }
}
