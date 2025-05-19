using FluentValidation;
using GymDdd.Framework.BaseTypes.Cqrs;

namespace GymManagement.Application.Usecases.Reservations.Commands;

public static class CreateReservationCommand
{
    public sealed record Request(
        Guid SessionId,
        Guid ParticipantId)
        : ICommand<Response>;

    public sealed record Response()
        : IResponse;

    internal sealed class Validator : AbstractValidator<Request>
    {
        public Validator()
        {
            RuleFor(x => x.SessionId)
                .NotEmpty();

            RuleFor(x => x.ParticipantId)
                .NotEmpty();
        }
    }

    //// TODO: LanguageExt
    //internal sealed class CreateReservationCommandUsecase
    //    : ICommandUsecase<CreateReservationCommand>
    //{
    //    private readonly ISessionsRepository _sessionsRepository;
    //    private readonly IParticipantsRepository _participantsRepository;

    //    public CreateReservationCommandUsecase(
    //        ISessionsRepository sessionsRepository,
    //        IParticipantsRepository participantsRepository)
    //    {
    //        _sessionsRepository = sessionsRepository;
    //        _participantsRepository = participantsRepository;
    //    }

    //    public async Task<IErrorOr> Handle(CreateReservationCommand command, CancellationToken cancellationToken)
    //    {
    //        Session? session = await _sessionsRepository.GetByIdAsync(command.SessionId);
    //        if (session is null)
    //        {
    //            return Error
    //                .NotFound(description: "Session not found")
    //                .ToErrorOr();
    //        }

    //        if (session.HasReservationForParticipant(command.ParticipantId))
    //        {
    //            return Error
    //                .Conflict(description: "Participant already has reservation")
    //                .ToErrorOr();
    //        }

    //        Participant? participant = await _participantsRepository.GetByIdAsync(command.ParticipantId);
    //        if (participant is null)
    //        {
    //            return Error
    //                .NotFound(description: "Participant not found")
    //                .ToErrorOr();
    //        }

    //        if (participant.HasReservationForSession(session.Id))
    //        {
    //            return Error
    //                .Conflict(description: "Participant not expected to have reservation to session")
    //                .ToErrorOr();
    //        }

    //        if (!participant.IsTimeShotFree(session.Date, session.Time))
    //        {
    //            return Error
    //                .Conflict(description: "Participant's calendar is not free for the entire session duration")
    //                .ToErrorOr();
    //        }

    //        ErrorOr<Success> reserveSpotResult = session.ReserveSpot(participant);

    //        if (reserveSpotResult.IsError)
    //        {
    //            return reserveSpotResult
    //                .Errors
    //                .ToErrorOr();
    //        }

    //        await _sessionsRepository.UpdateAsync(session);

    //        return Result
    //            .Success
    //            .ToErrorOr();
    //    }
    //}
}