using DddGym.Framework.BaseTypes.Application.Cqrs;

namespace GymManagement.Application.Usecases.Trainers.Commands.AddTrainer;

public sealed record AddTrainerCommand(
    Guid SubscriptionId,
    Guid GymId,
    Guid TrainerId)
    : ICommand;