using DddGym.Domain.AggregateRoots.Admins;

namespace DddGym.Application.Abstractions.Repositories;

public interface IAdminsRepository
{
    //Task AddAdminAsync(Admin participant);
    //Task<Profile?> GetProfileByUserIdAsync(Guid userId);
    Task<Admin?> GetByIdAsync(Guid adminId);
    Task UpdateAsync(Admin admin);
}
