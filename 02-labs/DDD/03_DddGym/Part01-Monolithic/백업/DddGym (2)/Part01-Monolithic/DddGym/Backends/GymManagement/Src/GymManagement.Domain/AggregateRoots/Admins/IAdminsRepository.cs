namespace GymManagement.Domain.AggregateRoots.Admins;

public interface IAdminsRepository
{
    FinT<IO, Unit> AddAdminAsync(Admin participant);

    //Task<Profile?> GetProfileByUserIdAsync(Guid userId);
    FinT<IO, Option<Admin>> GetByIdAsync(Guid adminId);

    FinT<IO, Unit> UpdateAsync(Admin admin);
}