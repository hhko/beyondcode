using DddGym.Framework.BaseTypes.Application.Events;
using DddGym.Framework.IntegrationEvents;
using GymManagement.Application.Abstractions.Repositories;
using GymManagement.Domain.AggregateRoots.Admins;

namespace GymManagement.Application.Usecases.Admins.IntegrationEvents.AdminProfileCreated;

internal sealed class AdminProfileCreatedEventUsecase
    : IDomainEventUsecase<AdminProfileCreatedIntegrationEvent>
{
    private readonly IAdminsRepository _adminsRepository;

    public AdminProfileCreatedEventUsecase(IAdminsRepository adminsRepository)
    {
        _adminsRepository = adminsRepository;
    }

    public async Task Handle(AdminProfileCreatedIntegrationEvent domainEvent, CancellationToken cancellationToken)
    {
        var admin = new Admin(domainEvent.UserId, domainEvent.AdminId);

        await _adminsRepository.AddAdminAsync(admin);
    }
}