using FluentValidation;
using GymDdd.Framework.BaseTypes.Cqrs;

namespace GymManagement.Application.Usecases.Profiles.Commands;

public static class CreateParticipantProfileCommand
{
    public sealed record Request(Guid UserId)
        : ICommandReqeust<Response>;

    public sealed record Response(Option<Guid> ParticipantId = default)
        : IResponse;

    internal sealed class Validator : AbstractValidator<Request>
    {
        public Validator()
        {
        }
    }

    internal sealed class Usecase(IUsersRepository usersRepository)
        : ICommandUsecase<Request, Response>
    {
        private readonly IUsersRepository _usersRepository = usersRepository;

        public async Task<Fin<Response>> Handle(Request request, CancellationToken cancellationToken)
        {
            var usecase = from user in _usersRepository.GetByIdAsync(request.UserId)
                          from newParticipantId in user.CreateParticipantProfile()
                          from _ in _usersRepository.UpdateAsync(user)
                          select newParticipantId;

            return await usecase
                .ToCreateParticipantProfileResponse();

            //return await usecase
            //    .Run()                                  // FinT<IO, Guid>    -> K<IO, Fin<Guid>>
            //    .RunAsync()                             // K<IO, Fin<Guid>>  -> Task<Fin<Guid>>
            //    .ToCreateParticipantProfileResponse();  // Task<Fin<Guid>>   -> Task<Fin<Response>>
        }
    }
}
