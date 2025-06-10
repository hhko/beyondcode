using GymManagement.Domain.AggregateRoots.Admins;

namespace GymManagement.Adapters.Persistence.Repositories;

public class AdminsRepository : IAdminsRepository
{
    public FinT<IO, Unit> AddAdminAsync(Admin participant)
    {
        return lift(() => unit);
    }

    public FinT<IO, Option<Admin>> GetByIdAsync(Guid adminId)
    {
        return lift(() => Option<Admin>.None);
    }

    public FinT<IO, Unit> UpdateAsync(Admin admin)
    {
        return lift(() => unit);
    }
}