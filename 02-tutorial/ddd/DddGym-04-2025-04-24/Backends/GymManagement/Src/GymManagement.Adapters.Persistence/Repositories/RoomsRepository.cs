using GymManagement.Domain.AggregateRoots.Rooms;
using LanguageExt;

namespace GymManagement.Adapters.Persistence.Repositories;

public class RoomsRepository : IRoomsRepository
{
    public Task AddRoomAsync(Room room)
    {
        throw new NotImplementedException();
    }

    //public Task<Room?> GetByIdAsync(Guid id)
    public Task<Fin<Room>> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<List<Room>> ListByGymIdAsync(Guid gymId)
    {
        throw new NotImplementedException();
    }

    public Task RemoveAsync(Room room)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Room room)
    {
        throw new NotImplementedException();
    }
}
