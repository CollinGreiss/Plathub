using Plathub.Models;

namespace Plathub.Interfaces
{
    public interface IDataAccessLayer
    {
        public List<long> GetGameIdsByUserId(string userId);
        public void AddGameToLibrary(string userId, long gameId);
        public IEnumerable<GameData> GetGameDataByUserId(string userId);
        public List<Profile> GetFriendsByUserId(string userId);
        public void AddFriend(string userId1, string userId2);
        public void AcceptFriendship(string userId1, string userId2);
        public Profile GetProfile(string userId);
        public ProfileData GetProfileData(string userId);
        public List<ProfileData> GetFriendsProfileData(string userId);
        public List<ProfileData> SearchProfilesByUsername(string searchString);
        public bool IsGameInLibrary(string userId, long gameId);
        public void RemoveFromLibrary(string userId, long gameId);
        public List<ProfileData> GetPendingFriends(string userId);
        public void UpdateProfile(Profile profile);
    }
}
