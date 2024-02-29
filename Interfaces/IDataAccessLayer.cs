namespace Plathub.Interfaces
{
    public interface IDataAccessLayer
    {
        public IEnumerable<long> GetGameIdsByUserId(string userId);
        public void AddGameToLibrary(string userId, long gameId);
    }
}
