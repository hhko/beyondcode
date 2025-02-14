using DddGym.Domain.Trainers;
using DddGym.Tests.Unit.LayerTests.Domain.Constants;

namespace DddGym.Tests.Unit.LayerTests.Domain.Factories;

public static class TrainerFactory
{
    public static Trainer CreateTrainer(
        Guid? userId = null,
        Guid? id = null)
    {
        return new Trainer(
            userId: userId ?? DomainConstants.User.Id,
            id: id ?? DomainConstants.Trainer.Id);
    }
}
