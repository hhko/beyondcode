using FluentValidation;
using FunctionalDdd.Framework.BaseTypes.Events;
using GymManagement.Domain.AggregateRoots.Admins;
using static GymManagement.Domain.AggregateRoots.Users.Events.DomainEvents;

namespace GymManagement.Application.Usecases.Admins.Events.AdminProfileCreated;

public static class AdminProfileCreatedEvent
{
    internal sealed class Validator
        : AbstractValidator<UserEvents.AdminProfileCreatedEvent>
    {

    }

    internal sealed class Usecase
        : IDomainEventUsecase<UserEvents.AdminProfileCreatedEvent>
    {
        private readonly IAdminsRepository _adminsRepository;

        public Usecase(IAdminsRepository adminsRepository)
        {
            _adminsRepository = adminsRepository;
        }

        public async Task Handle(UserEvents.AdminProfileCreatedEvent domainEvent, CancellationToken cancellationToken)
        {
            // TODO?: domainEvent.AdminId -> subscriptionId
            //var admin = new Admin(domainEvent.UserId, domainEvent.AdminId);
            Admin admin = Admin.Create(
                userId: domainEvent.UserId,
                id: domainEvent.AdminId);

            await _adminsRepository.AddAdminAsync(admin);
        }
    }
}