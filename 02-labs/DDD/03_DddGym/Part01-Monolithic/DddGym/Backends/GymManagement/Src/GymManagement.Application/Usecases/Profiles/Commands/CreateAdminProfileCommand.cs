using FluentValidation;
using GymDdd.Framework.BaseTypes.Cqrs;

namespace GymManagement.Application.Usecases.Profiles.Commands;

public static class CreateAdminProfileCommand
{
    public sealed record Request(Guid UserId)
        : ICommandReqeust<Response>;

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

    internal sealed class Usecase(IUsersRepository usersRepository)
        : ICommandUsecase<Request, Response>
    {
        private readonly IUsersRepository _usersRepository = usersRepository;

        public async Task<Fin<Response>> Handle(Request request, CancellationToken cancellationToken)
        {
            var usecase = from user in _usersRepository.GetByIdAsync(request.UserId)
                          from newAdminId in user.CreateAdminProfile()
                          from _ in _usersRepository.UpdateAsync(user)
                          select newAdminId;

            return await usecase
                .ToCreateAdminProfileResponse();
        }
    }
}