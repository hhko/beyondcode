using GymManagement.Domain.AggregateRoots.Rooms;

namespace GymManagement.Adapters.Persistence.Repositories;

public class RoomsRepository : IRoomsRepository
{
    public Task AddRoomAsync(Room room)
    {
        throw new NotImplementedException();
    }

    //public Task<Room?> GetByIdAsync(Guid id)
    public FinT<IO, Room> GetByIdAsync(Guid id)
    {
        return lift(() => Room.Create("", 10, Guid.NewGuid()));
    }

    public Task<List<Room>> ListByGymIdAsync(Guid gymId)
    {
        throw new NotImplementedException();
    }

    public Task RemoveAsync(Room room)
    {
        throw new NotImplementedException();
    }

    public FinT<IO, Unit> UpdateAsync(Room room)
    {
        return lift(() => unit);
    }
}
