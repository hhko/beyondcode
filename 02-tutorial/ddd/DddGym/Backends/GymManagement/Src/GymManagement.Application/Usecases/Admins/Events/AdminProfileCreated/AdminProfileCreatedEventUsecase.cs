using DddGym.Framework.BaseTypes.Events;
using GymManagement.Domain.AggregateRoots.Admins;
using GymManagement.Domain.AggregateRoots.Users.Events;

namespace GymManagement.Application.Usecases.Admins.Events.AdminProfileCreated;

internal sealed class AdminProfileCreatedEventUsecase
    : IDomainEventUsecase<AdminProfileCreatedEvent>
{
    private readonly IAdminsRepository _adminsRepository;

    public AdminProfileCreatedEventUsecase(IAdminsRepository adminsRepository)
    {
        _adminsRepository = adminsRepository;
    }

    public async Task Handle(AdminProfileCreatedEvent domainEvent, CancellationToken cancellationToken)
    {
        // TODO?: domainEvent.AdminId -> subscriptionId
        //var admin = new Admin(domainEvent.UserId, domainEvent.AdminId);
        Admin admin = Admin.Create(
            userId: domainEvent.UserId, 
            id: domainEvent.AdminId);

        await _adminsRepository.AddAdminAsync(admin);
    }
}