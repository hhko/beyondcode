﻿using FluentValidation;
using GymDdd.Framework.BaseTypes.Cqrs;

namespace GymManagement.Application.Usecases.Participants.Commands;

public static class CancelReservationCommand
{
    public sealed record Request(
        Guid ParticipantId,
        Guid SessionId)
        : ICommandReqeust<Response>;

    public sealed record Response()
        : IResponse;

    internal sealed class Validator : AbstractValidator<Request>
    {
        public Validator()
        {
        }
    }

    //// TODO: LanguageExt
    //internal sealed class CancelReservationCommandUsecase
    //    : ICommandUsecase<CancelReservationCommand>
    //{
    //    private readonly ISessionsRepository _sessionsRepository;
    //    private readonly IParticipantsRepository _participantsRepository;
    //    private readonly IDateTimeProvider _dateTimeProvider;

    //    public CancelReservationCommandUsecase(
    //        ISessionsRepository sessionsRepository,
    //        IParticipantsRepository participantsRepository,
    //        IDateTimeProvider dateTimeProvider)
    //    {
    //        _sessionsRepository = sessionsRepository;
    //        _participantsRepository = participantsRepository;
    //        _dateTimeProvider = dateTimeProvider;
    //    }

    //    public async Task<IErrorOr> Handle(CancelReservationCommand command, CancellationToken cancellationToken)
    //    {
    //        Session? session = await _sessionsRepository.GetByIdAsync(command.SessionId);
    //        if (session is null)
    //        {
    //            return Error
    //                .NotFound(description: "Session not found")
    //                .ToErrorOr();
    //        }

    //        if (!session.HasReservationForParticipant(command.ParticipantId))
    //        {
    //            return Error
    //                .NotFound(description: "User doesn't have a reservation for the given session")
    //                .ToErrorOr<ListParticipantSessionsResponse>();
    //        }

    //        Participant? participant = await _participantsRepository.GetByIdAsync(command.ParticipantId);
    //        if (participant is null)
    //        {
    //            return Error
    //                .NotFound(description: "Participant not found")
    //                .ToErrorOr();
    //        }

    //        if (!participant.HasReservationForSession(session.Id))
    //        {
    //            return Error
    //                .Unexpected(description: "Participant expected to have a reservation to session")
    //                .ToErrorOr<ListParticipantSessionsResponse>();
    //        }

    //        ErrorOr<Success> reserveSpotResult = session.CancelReservation(command.ParticipantId, _dateTimeProvider);
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