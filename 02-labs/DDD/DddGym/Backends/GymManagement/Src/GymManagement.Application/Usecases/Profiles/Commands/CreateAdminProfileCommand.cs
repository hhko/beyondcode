using FluentValidation;
using GymDdd.Framework.BaseTypes.Cqrs;
using GymManagement.Domain.AggregateRoots.Users;

namespace GymManagement.Application.Usecases.Profiles.Commands;

public static class CreateAdminProfileCommand
{
    public sealed record Request(Guid UserId)
        : ICommand<Response>;

    public sealed record Response(Option<Guid> AdminId)
        : IResponse;

    internal sealed class Validator
        : AbstractValidator<Request>
    {
        public Validator()
        {
            RuleFor(_ => _.UserId)
                .NotEmpty();
        }
    }

    internal sealed class Telemetry
    {

    }

    internal sealed class Usecase(IUsersRepository usersRepository)
        : ICommandUsecase<Request, Response>
    {
        private readonly IUsersRepository _usersRepository = usersRepository;

        public async Task<Fin<Response>> Handle(Request request, CancellationToken cancellationToken)
        {
            FinT<IO, Guid> usecase = from user in _usersRepository.GetByIdAsync(request.UserId)
                                     from adminId in user.CreateAdminProfile()
                                     from _ in _usersRepository.UpdateAsync(user)
                                     select adminId;

            return await usecase
                .Run()
                .RunAsync()
                .ToCreateAdminProfileResponse();
        }
    }
}