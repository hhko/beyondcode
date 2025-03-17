using GymManagement.Domain.AggregateRoots.Admins;

namespace GymManagement.Adapters.Persistence.Repositories;

public class AdminsRepository : IAdminsRepository
{
    public Task AddAdminAsync(Admin participant)
    {
        throw new NotImplementedException();
    }

    public Task<Admin?> GetByIdAsync(Guid adminId)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Admin admin)
    {
        throw new NotImplementedException();
    }
}