using FluentValidation;
using GymDdd.Framework.BaseTypes.Cqrs;
using static GymManagement.Application.Usecases.Profiles.Errors.ApplicationErrors;

namespace GymManagement.Application.Usecases.Profiles.Commands;

public static class CreateTrainerProfileCommand
{
    public sealed record Request(Guid UserId)
        : ICommandReqeust<Response>;

    public sealed record Response(Option<Guid> TrainerId)
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
                                                       .ToRequiredOrError(CreateTrainerProfileErrors.UserIdNotFound(request.UserId))
                          from newTrainerId in user.CreateTrainerProfile()
                          from _ in _usersRepository.UpdateAsync(user)
                          select newTrainerId;

            return await usecase
                .ToCreateTrainerProfileResponse();
        }
    }
}