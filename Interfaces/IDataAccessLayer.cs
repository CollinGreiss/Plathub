using Plathub.Models;

namespace Plathub.Interfaces
{
    public interface IDataAccessLayer
    {
        public List<long> GetGameIdsByUserId(string userId);
        public void AddGameToLibrary(string userId, long gameId);
        public IEnumerable<GameData> GetGameDataByUserId(string userId);
    }
}
