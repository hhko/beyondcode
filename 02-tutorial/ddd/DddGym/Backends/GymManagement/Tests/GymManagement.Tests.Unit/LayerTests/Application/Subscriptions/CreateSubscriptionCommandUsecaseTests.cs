using static GymManagement.Tests.Unit.Abstractions.Constants.Constants;

namespace GymManagement.Tests.Unit.LayerTests.Application.Subscriptions;

[Trait(nameof(UnitTest), UnitTest.Application)]
public class CreateSubscriptionCommandUsecaseTests
{
    //[Fact]
    //public async Task Handle_ShouldSucceed()
    //{
    //    // Arrange - ServiceCollection
    //    ServiceCollection services = new ServiceCollection();
    //    services.RegisterApplication();

    //    // Arrange - IAdminsRepository::GetByIdAsync
    //    Admin admin = new(userId: Guid.NewGuid());
    //    IAdminsRepository adminsRepository = Substitute.For<IAdminsRepository>();
    //    adminsRepository.GetByIdAsync(Arg.Any<Guid>())
    //        .Returns(admin);
    //    services.AddSingleton(adminsRepository);

    //    using ServiceProvider provider = services.BuildServiceProvider();
    //    ISender sut = provider.GetRequiredService<ISender>();

    //    // Act
    //    var actual = await sut.Send(new CreateSubscriptionCommand(
    //        SubscriptionType.Pro,
    //        admin.Id));

    //    // Assert
    //    actual.IsError.ShouldBeFalse();
    //    actual.Value.Subscription.ShouldNotBeNull();
    //    actual.Value.Subscription.Id.ShouldBe(admin.SubscriptionId!.Value);
    //}

    //[Fact]
    //public async Task Handle_WhenAdminNotFound_ShouldFail()
    //{
    //    // Arrange - ServiceCollection
    //    ServiceCollection services = new ServiceCollection();
    //    services.RegisterApplication();

    //    // Arrange - IAdminsRepository::GetByIdAsync
    //    IAdminsRepository adminsRepository = Substitute.For<IAdminsRepository>();
    //    services.AddSingleton(adminsRepository);

    //    using ServiceProvider provider = services.BuildServiceProvider();
    //    ISender sut = provider.GetRequiredService<ISender>();

    //    // Act
    //    var actual = await sut.Send(new CreateSubscriptionCommand(
    //        SubscriptionType: SubscriptionType.Pro,
    //        AddminId: Guid.NewGuid()));

    //    // Assert
    //    actual.IsError.ShouldBeTrue();
    //    // .NotFound(description: "Admin not found")
    //    actual.Errors.ShouldNotBeEmpty();
    //    actual.Errors![0].Code.ShouldBe("General.NotFound");
    //}

    //[Fact]
    //public async Task Handle_WhenSubscriptionIsAlreadyExist_ShouldFail()
    //{
    //    // Arrange - ServiceCollection
    //    ServiceCollection services = new ServiceCollection();
    //    services.RegisterApplication();

    //    // Arrange - IAdminsRepository::GetByIdAsync
    //    Admin admin = AdminFactory.CreateAdmin(subscriptionId: Guid.NewGuid());

    //    IAdminsRepository adminsRepository = Substitute.For<IAdminsRepository>();
    //    adminsRepository
    //        .GetByIdAsync(Arg.Any<Guid>())
    //        .Returns(admin);
    //    services.AddSingleton(adminsRepository);

    //    using ServiceProvider provider = services.BuildServiceProvider();
    //    ISender sut = provider.GetRequiredService<ISender>();

    //    // Act
    //    var actual = await sut.Send(new CreateSubscriptionCommand(
    //        SubscriptionType: SubscriptionType.Pro,
    //        AddminId: Guid.NewGuid()));

    //    // Assert
    //    actual.IsError.ShouldBeTrue();
    //    // .Conflict(description: "Admin already has active subscription")
    //    actual.Errors.ShouldNotBeEmpty();
    //    actual.Errors![0].Code.ShouldBe("General.Conflict");
    //}
}