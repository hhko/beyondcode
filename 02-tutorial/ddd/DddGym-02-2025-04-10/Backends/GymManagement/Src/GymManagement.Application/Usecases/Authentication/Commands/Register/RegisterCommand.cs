using DddGym.Framework.BaseTypes.Cqrs;

namespace GymManagement.Application.Usecases.Authentication.Commands.Register;

public sealed record RegisterCommand(
    string FirstName,
    string LastName,
    string Email,
    string Password) : ICommand2<RegisterResponse>; //ICommand<RegisterResponse>;