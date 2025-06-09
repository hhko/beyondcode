﻿using DddGym.Framework.BaseTypes.Cqrs;
using ErrorOr;
using GymManagement.Domain.AggregateRoots.Gyms;

namespace GymManagement.Application.Usecases.Rooms.Commands.DeleteRoom;

internal sealed class DeleteRoomCommandUsecase
    : ICommandUsecase<DeleteRoomCommand, DeleteRoomResponse>
{
    private readonly IGymsRepository _gymsRepository;

    public DeleteRoomCommandUsecase(IGymsRepository gymsRepository)
    {
        _gymsRepository = gymsRepository;
    }

    public async Task<IErrorOr<DeleteRoomResponse>> Handle(DeleteRoomCommand command, CancellationToken cancellationToken)
    {
        Gym? gym = await _gymsRepository.GetByIdAsync(command.GymId);
        if (gym is null)
        {
            return Error
                .NotFound(description: "Gym not found")
                .ToErrorOr<DeleteRoomResponse>();
        }

        if (gym.HasRoom(command.RoomId) is false)
        {
            return Error
                .NotFound(description: "Room not found")
                .ToErrorOr<DeleteRoomResponse>();
        }

        gym.RemoveRoom(command.RoomId);

        await _gymsRepository.UpdateAsync(gym);

        return Result.Deleted
            .ToResponseDeleted()
            .ToErrorOr();
    }
}