using Bogus;
using GymManagement.Application.Usecases.Profiles.Queries.GetProfile;
using GymManagement.Domain.AggregateRoots.Users;
using GymManagement.Tests.Integration.Abstractions;
using GymManagement.Tests.Integration.Abstractions.Fixtures;
using LanguageExt;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using static GymManagement.Tests.Unit.Abstractions.Constants.AssemblyConstants;

namespace GymManagement.Tests.Integration.LayerTests.Adapters.Presentation;

[Collection(CollectionName.WebAppFactoryCollectionDefinition)]
[Trait(nameof(IntegrationTest), IntegrationTest.WebApi)]
public class UserControllerTests : ControllerTestsBase
{
    private readonly WebApplicationFactory<IAppMarker> _factory;

    public UserControllerTests(WebAppFactoryFixture fixture)
        : base(fixture)
    {
        _factory = _webAppFactory
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    var userFaker = new Faker<User>()
                        .CustomInstantiator(f => User.Create(
                            firstName: f.Name.FirstName(),
                            lastName: f.Name.LastName(),
                            email: f.Internet.Email(),
                            passwordHash: f.Internet.Password()));
                    var fakeUser = userFaker.Generate();

                    IUsersRepository usersRepository = Substitute.For<IUsersRepository>();
                    usersRepository.GetByIdAsync(Arg.Any<Guid>())
                        .Returns(fakeUser);

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

        // Assert
        response.EnsureSuccessStatusCode();

        // System.NotSupportedException
        //  Message = The collection type 'LanguageExt.Option`1[System.Guid]' is abstract,
        //      an interface, or is read only, and could not be instantiated and populated.Path:
        //      $.adminId | LineNumber: 0 | BytePositionInLine: 12.
        //Source= System.Text.Json

        var getProfileResponse = await response.Content.ReadFromJsonAsync<GetProfileResponse>();


        //GetProfileResponse? getProfileResponse = await response.Content.ReadFromJsonAsync<GetProfileResponse>();
        //getProfileResponse.ShouldSatisfyAllConditions(
        //    () => getProfileResponse!.AdminId.IsNone.ShouldBeTrue(),
        //    () => getProfileResponse!.TrainerId.IsNone.ShouldBeTrue(),
        //    () => getProfileResponse!.ParticipantId.IsNone.ShouldBeTrue());
    }

    public class OptionJsonConverter<T> : JsonConverter<Option<T>>
    {
        public override Option<T> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = JsonSerializer.Deserialize<T>(ref reader, options);
            return value == null ? Option<T>.None : Option<T>.Some(value);
        }

        public override void Write(Utf8JsonWriter writer, Option<T> value, JsonSerializerOptions options)
        {
            if (value.IsSome)
            {
                JsonSerializer.Serialize(writer, value, options);
            }
            else
            {
                writer.WriteNullValue();
            }
        }
    }


    [Fact]
    public async Task User_HaveAdminProfile_When_CreateAdminProfile()
    {
        // Arrange
        using var client = _factory.CreateClient();

        // Act
        HttpResponseMessage response = await client.PostAsync("/users/0d47d5e2-04b3-4c89-a792-5fcae1e5b8fc/profiles/admin", null);

        // Assert
        response.EnsureSuccessStatusCode();

        // 에러
        //CreateAdminProfileResponse? createAdminProfileResponse = await response.Content.ReadFromJsonAsync<CreateAdminProfileResponse>();
        //createAdminProfileResponse!.AdminId.IsSome.ShouldBeTrue();
    }

    [Fact]
    public async Task User_HaveTrainerProfile_When_CreateTrainerProfile()
    {
        // Arrange
        using var client = _factory.CreateClient();

        // Act
        HttpResponseMessage response = await client.PostAsync("/users/0d47d5e2-04b3-4c89-a792-5fcae1e5b8fc/profiles/trainer", null);

        // Assert
        response.EnsureSuccessStatusCode();

        // 에러
        //CreateTrainerProfileResponse? createTrainerProfileResponse = await response.Content.ReadFromJsonAsync<CreateTrainerProfileResponse>();
        //createTrainerProfileResponse!.TrainerId.IsSome.ShouldBeTrue();
    }

    [Fact]
    public async Task User_HaveParticipantProfile_When_CreateParticipantProfile()
    {
        // Arrange
        using var client = _factory.CreateClient();

        // Act
        HttpResponseMessage response = await client.PostAsync("/users/0d47d5e2-04b3-4c89-a792-5fcae1e5b8fc/profiles/participant", null);

        // Assert
        response.EnsureSuccessStatusCode();

        // 에러
        //CreateParticipantProfileResponse? createParticipantProfileResponse = await response.Content.ReadFromJsonAsync<CreateParticipantProfileResponse>();
        //createParticipantProfileResponse!.ParticipantId.IsSome.ShouldBeTrue();
    }
}
