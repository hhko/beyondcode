using Bogus;
using GymDdd.Framework.BaseTypes.Converters;
using GymManagement.Application.Usecases.Profiles;
using GymManagement.Application.Usecases.Profiles.Commands;
using GymManagement.Application.Usecases.Profiles.Queries;
using GymManagement.Domain.AggregateRoots.Users;
using GymManagement.Tests.Integration.Abstractions;
using GymManagement.Tests.Integration.Abstractions.Fixtures;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Json;
using System.Text.Json;
using static GymManagement.Tests.Unit.Abstractions.Constants.AssemblyConstants;

namespace GymManagement.Tests.Integration.LayerTests.Adapters.Presentation;

[Collection(CollectionName.WebAppFactoryCollectionDefinition)]
[Trait(nameof(IntegrationTest), IntegrationTest.WebApi)]
public class UserControllerTests : ControllerTestsBase
{
    private readonly WebApplicationFactory<IAppMarker> _factory;
    private readonly JsonSerializerOptions _options;

    public UserControllerTests(WebAppFactoryFixture fixture)
        : base(fixture)
    {
        _options = new JsonSerializerOptions();
        _options.Converters.Add(new OptionJsonConverterFactory());
        _options.PropertyNameCaseInsensitive = true;

        _factory = _webAppFactory
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    var userFaker = new Faker<User>()
                        .CustomInstantiator(f => (User)User.Create(
                            firstName: f.Name.FirstName(),
                            lastName: f.Name.LastName(),
                            email: f.Internet.Email(),
                            passwordHash: f.Internet.Password()));
                    var fakeUser = userFaker.Generate();

                    IUsersRepository usersRepository = Substitute.For<IUsersRepository>();
                    usersRepository.GetByIdAsync(Arg.Any<Guid>())
                        .Returns(lift(() => fakeUser));

                    usersRepository.UpdateAsync(Arg.Any<User>())
                        .Returns(lift(() => unit));

                    services.AddScoped(_ => usersRepository);
                });
            });
    }

    // 사용자가 등록되면 모든 Profile은 None 상태입니다.
    [Fact]
    public async Task User_HaveNoneProfiles_When_UserIsRegisered()
    {
        // Arrange
        using var client = _factory.CreateClient();

        // Act
        HttpResponseMessage response = await client.GetAsync("/users/0d47d5e2-04b3-4c89-a792-5fcae1e5b8fc/profiles");

        // Assert: HTTP 결과 상태
        response.EnsureSuccessStatusCode();

        // Assert: HTTP 결과 값
        GetProfileQuery.Response? getProfileResponse = await response.Content.ReadFromJsonAsync<GetProfileQuery.Response>(_options);
        getProfileResponse.ShouldSatisfyAllConditions(
            () => getProfileResponse!.AdminId.IsNone.ShouldBeTrue(),
            () => getProfileResponse!.TrainerId.IsNone.ShouldBeTrue(),
            () => getProfileResponse!.ParticipantId.IsNone.ShouldBeTrue());
    }

    [Fact]
    public async Task User_HaveAdminProfile_When_CreateAdminProfile()
    {
        // Arrange
        using var client = _factory.CreateClient();

        // Act
        HttpResponseMessage response = await client.PostAsync("/users/0d47d5e2-04b3-4c89-a792-5fcae1e5b8fc/profiles/admin", null);

        // Assert: HTTP 결과 상태
        response.EnsureSuccessStatusCode();

        // Assert: HTTP 결과 값
        CreateAdminProfileCommand.Response? createAdminProfileResponse = await response.Content.ReadFromJsonAsync<CreateAdminProfileCommand.Response>(_options)!;
        createAdminProfileResponse!.AdminId.IsSome.ShouldBeTrue();
    }


    [Fact]
    public async Task User_HaveTrainerProfile_When_CreateTrainerProfile()
    {
        // Arrange
        using var client = _factory.CreateClient();

        // Act
        HttpResponseMessage response = await client.PostAsync("/users/0d47d5e2-04b3-4c89-a792-5fcae1e5b8fc/profiles/trainer", null);

        // Assert: HTTP 결과 상태
        response.EnsureSuccessStatusCode();

        // Assert: HTTP 결과 값
        CreateTrainerProfileCommand.Response? createTrainerProfileResponse = await response.Content.ReadFromJsonAsync<CreateTrainerProfileCommand.Response>(_options)!;
        createTrainerProfileResponse!.TrainerId.IsSome.ShouldBeTrue();
    }

    [Fact]
    public async Task User_HaveParticipantProfile_When_CreateParticipantProfile()
    {
        // Arrange
        using var client = _factory.CreateClient();

        // Act
        HttpResponseMessage response = await client.PostAsync("/users/0d47d5e2-04b3-4c89-a792-5fcae1e5b8fc/profiles/participant", null);

        // Assert: HTTP 결과 상태
        response.EnsureSuccessStatusCode();

        // Assert: HTTP 결과 값
        CreateParticipantProfileCommand.Response? createParticipantProfileResponse = await response.Content.ReadFromJsonAsync<CreateParticipantProfileCommand.Response>(_options)!;
        createParticipantProfileResponse!.ParticipantId.IsSome.ShouldBeTrue();
    }
}
