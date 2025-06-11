using Bogus;
using GymManagement.Adapters.Infrastructure.Authentication.TokenGenerator;
using GymManagement.Domain.AggregateRoots.Users;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using static GymManagement.Tests.Unit.Abstractions.Constants.Constants;

namespace GymManagement.Tests.Unit.LayerTests.Adapters.Infrastructure.Authentication;

[Trait(nameof(UnitTest), UnitTest.Infrastructure)]
public sealed class TokenGeneratorTests
{
    private readonly JwtOptions _options = new()
    {
        Audience = "TestAudience",
        Issuer = "TestIssuer",
        Secret = "SuperSecretKey1234567890!@#Extra", // 최소 32자 이상
        TokenExpirationInMinutes = 30,
    };

    private readonly User _user = (User)User.Create(
        firstName: "Test",
        lastName: "Test",
        email: "test@example.com",
        passwordHash: "xyz");

    public FinT<IO, Option<User>> GetByIdAsync(Guid userId)
    {
        return IO.liftAsync(async () =>
        {
            await Task.CompletedTask;

            var userFaker = new Faker<Fin<User>>()
                            .CustomInstantiator(f => User.Create(
                                firstName: f.Name.FirstName(),
                                lastName: f.Name.LastName(),
                                email: f.Internet.Email(),
                                passwordHash: f.Internet.Password()));

            return userFaker.Generate()
                            .Map(user => Option<User>.Some(user));
        });
    }

    [Fact]
    public async Task GenerateToken_ShouldReturnSuccess_WhenUserIsValid()
    {
        // Arrange
        JwtTokenGenerator generator = new(Options.Create(_options));

        // Act
        var actual = generator.GenerateToken(_user);
        var result = await actual
            .Run()
            .RunAsync();

        // Assert
        result.IsSucc.ShouldBeTrue();
        result.Match(
            Succ: token => token.ShouldNotBeNullOrEmpty(),
            Fail: _ => throw new Xunit.Sdk.XunitException("Expected success but got failure")
        );

        //// Header
        //{
        //    "alg": "HS256",
        //    "typ": "JWT"
        //}
        //
        //// Payload
        //{
        //    "name": "Test",
        //    "email": "test@example.com",
        //    "id": "59bb7e2a-9e58-4638-9079-c3da87ca3b2d",
        //    "nbf": 1749605748,
        //    "exp": 1749607548,
        //    "iss": "TestIssuer",
        //    "aud": "TestAudience"
        //}
    }

    [Fact]
    public async Task GenerateToken_ShouldContainExpectedClaims()
    {
        // Arrange
        JwtTokenGenerator generator = new(Options.Create(_options));

        // Act
        var actual = generator.GenerateToken(_user);
        var result = await actual
            .Run()
            .RunAsync();

        var token = result.IfFail(string.Empty);

        // Assert
        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadJwtToken(token);

        jwtToken.Claims.ShouldContain(c => c.Type == JwtRegisteredClaimNames.Name && c.Value == _user.FirstName);
        jwtToken.Claims.ShouldContain(c => c.Type == JwtRegisteredClaimNames.Email && c.Value == _user.Email);
        jwtToken.Claims.ShouldContain(c => c.Type == "id" && c.Value == _user.Id.ToString());
    }

    //[Fact]
    //public async Task GenerateToken_ShouldReturnFailure_WhenUserIsNull()
    //{
    //    // Arrange
    //    var generator = CreateGenerator();

    //    // Act
    //    var actual = generator.GenerateToken(_user);
    //    var result = await actual
    //        .Run()
    //        .RunAsync();

    //    // Assert
    //    result.IsFail.Should().BeTrue();
    //}
}
