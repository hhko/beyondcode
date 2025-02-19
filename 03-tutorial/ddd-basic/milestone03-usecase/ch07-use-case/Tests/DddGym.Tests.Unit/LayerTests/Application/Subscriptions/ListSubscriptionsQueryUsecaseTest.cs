using DddGym.Application.Abstractions.Registrations;
using DddGym.Application.Usecases.Subscriptions.Queries.ListSubscriptions;
using DddGym.Domain.AggregateRoots.Subscriptions;
using DddGym.Tests.Unit.LayerTests.Domain.Factories;
using ErrorOr;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Shouldly;

namespace DddGym.Tests.Unit.LayerTests.Application.Subscriptions;

public class ListSubscriptionsQueryUsecaseTest
{
    [Fact]
    public async Task ShouldSucceed()
    {
        // Arrange: Application 레이어 의존성 주입
        ServiceCollection services = new ServiceCollection();
        services.RegisterApplication();

        // Arrange: Persistance 레이어 의존성 주입
        //  - ISubscriptionRepository
        ISubscriptionRepository subscriptionRepository = Substitute.For<ISubscriptionRepository>();
        subscriptionRepository.ListAsync().Returns([
            SubscriptionFactory.CreateSubscription()
        ]);
        services.AddSingleton(subscriptionRepository);

        // Arrange: sut 객체
        using ServiceProvider provider = services.BuildServiceProvider();
        ISender sut = provider.GetRequiredService<ISender>();

        // Act
        IErrorOr<SubscriptionsResponse> actual = await sut.Send(new ListSubscriptionsQuery(Name: ""));

        // Assert
        actual.IsError.ShouldBeFalse();
        actual.Value.Subscriptions.ShouldNotBeEmpty();
        actual.Value.Subscriptions.Count.ShouldBe(1);
    }
}
