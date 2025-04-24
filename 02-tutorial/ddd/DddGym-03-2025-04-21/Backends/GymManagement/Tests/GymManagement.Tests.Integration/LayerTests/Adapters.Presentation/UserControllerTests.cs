using Bogus;
using GymManagement.Application.Usecases.Profiles.Queries.GetProfile;
using GymManagement.Domain.AggregateRoots.Admins;
using GymManagement.Domain.AggregateRoots.Users;
using GymManagement.Tests.Integration.Abstractions;
using GymManagement.Tests.Integration.Abstractions.Fixtures;
using LanguageExt;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Json;
using static GymManagement.Tests.Unit.Abstractions.Constants.AssemblyConstants;

namespace GymManagement.Tests.Integration.LayerTests.Adapters.Presentation;

[Collection(CollectionName.WebAppFactoryCollectionDefinition)]
[Trait(nameof(IntegrationTest), IntegrationTest.WebApi)]
public class UserControllerTests : ControllerTestsBase
{
    public UserControllerTests(WebAppFactoryFixture fixture)
        : base(fixture)
    {

    }

    // 사용자가 등록되면 모든 Profile은 None 상태입니다.
    [Fact]
    public async Task User_HaveNoneProfiles_When_UserIsRegisered()
    {
        // Arrange
        using var client = _webAppFactory
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    var userFaker = new Faker<User>()
                        .CustomInstantiator(f => User.Create(
                            firstName:      f.Name.FirstName(),
                            lastName:       f.Name.LastName(),
                            email:          f.Internet.Email(),
                            passwordHash:   f.Internet.Password()));
                    var fakeUser = userFaker.Generate();

                    IUsersRepository usersRepository = Substitute.For<IUsersRepository>();
                    usersRepository.GetByIdAsync(Arg.Any<Guid>())
                        .Returns(fakeUser);
                    
                    services.AddScoped(_ => usersRepository);
                });
            })
            .CreateClient();

        // Act
        HttpResponseMessage response = await client.GetAsync("/users/0d47d5e2-04b3-4c89-a792-5fcae1e5b8fc/profiles");
        
        // Assert
        response.EnsureSuccessStatusCode();

        GetProfileResponse? getProfileResponse = await response.Content.ReadFromJsonAsync<GetProfileResponse>();
        getProfileResponse.ShouldSatisfyAllConditions(
            () => getProfileResponse!.AdminId.IsNone.ShouldBeTrue(),
            () => getProfileResponse!.TrainerId.IsNone.ShouldBeTrue(),
            () => getProfileResponse!.ParticipantId.IsNone.ShouldBeTrue());
    }
}
