using GymManagement.Domain.AggregateRoots.Trainers;
using GymManagement.Tests.Unit.LayerTests.Domain.Constants;

namespace GymManagement.Tests.Unit.LayerTests.Domain.Factories;

internal static class TrainerFactory
{
    public static Trainer CreateTrainer(
        Option<Guid> userId = default,
        Option<Guid> id = default)
    {
        return Trainer.Create(
            userId: userId.IfNone(DomainConstants.User.Id),
            id: id.IfNone(DomainConstants.Trainer.Id));
    }
}