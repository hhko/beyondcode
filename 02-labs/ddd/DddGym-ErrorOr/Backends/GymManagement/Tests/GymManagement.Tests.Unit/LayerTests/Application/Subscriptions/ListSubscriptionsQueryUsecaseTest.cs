namespace GymManagement.Tests.Unit.LayerTests.Application.Subscriptions;

//[Trait(nameof(UnitTest), UnitTest.Application)]
//public class ListSubscriptionsQueryUsecaseTest
//{
//    [Fact]
//    public async Task Handle_ShouldSucceed()
//    {
//        // Arrange: Application 레이어 의존성 주입
//        ServiceCollection services = new ServiceCollection();
//        services.RegisterApplication();

//        // Arrange: Persistance 레이어 의존성 주입
//        //  - ISubscriptionRepository
//        ISubscriptionsRepository subscriptionRepository = Substitute.For<ISubscriptionsRepository>();
//        subscriptionRepository.ListAsync().Returns([
//            SubscriptionFactory.CreateSubscription()
//        ]);
//        services.AddSingleton(subscriptionRepository);

//        // Arrange: sut 객체
//        using ServiceProvider provider = services.BuildServiceProvider();
//        ISender sut = provider.GetRequiredService<ISender>();

//        // Act
//        IErrorOr<ListSubscriptionsResponse> actual = await sut.Send(new ListSubscriptionsQuery());

//        // Assert
//        actual.IsError.ShouldBeFalse();
//        actual.Value.Subscriptions.ShouldNotBeEmpty();
//        actual.Value.Subscriptions.Count.ShouldBe(1);
//    }
//}