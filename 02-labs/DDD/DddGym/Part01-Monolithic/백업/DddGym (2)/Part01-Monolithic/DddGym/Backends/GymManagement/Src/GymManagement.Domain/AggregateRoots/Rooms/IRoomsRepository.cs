namespace GymManagement.Domain.AggregateRoots.Rooms;

public interface IRoomsRepository
{
    Task AddRoomAsync(Room room);
    //Task<Room?> GetByIdAsync(Guid id);
    FinT<IO, Room> GetByIdAsync(Guid id);
    Task<List<Room>> ListByGymIdAsync(Guid gymId);
    Task RemoveAsync(Room room);
    FinT<IO, Unit> UpdateAsync(Room room);
}
