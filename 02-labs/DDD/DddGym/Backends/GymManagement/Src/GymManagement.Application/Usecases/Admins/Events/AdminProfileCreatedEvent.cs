﻿using GymDdd.Framework.BaseTypes.Events;
using GymManagement.Domain.AggregateRoots.Admins;
using static GymManagement.Domain.AggregateRoots.Users.Events.DomainEvents;

namespace GymManagement.Application.Usecases.Admins.Events.AdminProfileCreated;

public static class AdminProfileCreatedEvent
{
    internal sealed class Usecase(IAdminsRepository adminsRepository)
        : IDomainEventUsecase<UserEvents.AdminProfileCreatedEvent>
    {
        private readonly IAdminsRepository _adminsRepository = adminsRepository;

        public async Task Handle(UserEvents.AdminProfileCreatedEvent domainEvent, CancellationToken cancellationToken)
        {
            var usecase = from admin in Admin.Create(userId: domainEvent.UserId, id: domainEvent.AdminId)
                          from _ in _adminsRepository.AddAdminAsync(admin)
                          select unit;

            await usecase
                .Run()
                .RunAsync();
        }
    }
}