using IGDB.Models;
using Plathub.Interfaces;
using Plathub.Models;
using Plathub.APIs;
using System;
using System.Linq;

namespace Plathub.Data
{
    public class UserGameDAL : IDataAccessLayer
    {
        private ApplicationDbContext db;
        public UserGameDAL(ApplicationDbContext indb)
        {
            db = indb;
        }

        public void AddGameToLibrary(string userId, long gameId)
        {
            UserGame model = new UserGame(userId, gameId);
            db.UserGames.Add(model);
            db.SaveChanges();
        }

        public void RemoveFromLibrary(string userId, long gameId)
        {
            var userGame = db.UserGames.FirstOrDefault(x => x.UserId == userId && x.GameId == gameId);
            if (userGame != null)
            {
                db.UserGames.Remove(userGame);
                db.SaveChanges();
            }
        }

        public List<long> GetGameIdsByUserId(string userId)
        {
            return db.UserGames.Where(x => x.UserId == userId).Select(x => x.GameId).ToList();
        }

        public IEnumerable<GameData> GetGameDataByUserId(string userId)
        {
            List<GameData> gamelist = new List<GameData>();
            List<long> gameIds = GetGameIdsByUserId(userId);
            foreach (var gameId in gameIds)
            {
                var task = GamesAPI.GetGame((int)gameId);
                task.Wait();
                GameData gameData = new GameData(task.Result);
                gamelist.Add(gameData);
            }
            return gamelist;
        }

		public List<Profile> GetFriendsByUserId(string userId)
		{
			var friendIds = db.Friendships
							   .Where(f => (f.UserId1 == userId || f.UserId2 == userId) && f.Accepted)
							   .Select(f => f.UserId1 == userId ? f.UserId2 : f.UserId1)
							   .ToList();

			var friendProfiles = db.Profiles
								   .Where(p => friendIds.Contains(p.UserId))
								   .ToList();

			return friendProfiles;
		}
        public List<ProfileData> GetFriendRequests(string userId)
        {
            var friendIds = db.Friendships
                               .Where(f => (f.UserId2 == userId) && !f.Accepted)
                               .Select(f => f.UserId1)
                               .ToList();

            var friendsProfileData = new List<ProfileData>();

            foreach (var friendId in friendIds)
            {
                var friendProfileData = GetProfileData(friendId);
                if (friendProfileData != null)
                {
                    friendsProfileData.Add(friendProfileData);
                }
            }

            return friendsProfileData;
        }
        public List<ProfileData> GetPendingFriends(string userId)
        {
            var friendIds = db.Friendships
                               .Where(f => (f.UserId1 == userId) && !f.Accepted)
                               .Select(f => f.UserId2)
                               .ToList();

            var friendsProfileData = new List<ProfileData>();

            foreach (var friendId in friendIds)
            {
                var friendProfileData = GetProfileData(friendId);
                if (friendProfileData != null)
                {
                    friendsProfileData.Add(friendProfileData);
                }
            }

            return friendsProfileData;
        }
        public List<ProfileData> GetFriendsProfileData(string userId)
        {
            var friendIds = GetFriendsByUserId(userId).Select(f => f.UserId).ToList();
            var friendsProfileData = new List<ProfileData>();

            foreach (var friendId in friendIds)
            {
                var friendProfileData = GetProfileData(friendId);
                if (friendProfileData != null)
                {
                    friendsProfileData.Add(friendProfileData);
                }
            }

            return friendsProfileData;
        }

        public void AddFriend(string userId1, string userId2)
		{
			var existingFriendship = db.Friendships.FirstOrDefault(f => (f.UserId1 == userId1 && f.UserId2 == userId2) || (f.UserId1 == userId2 && f.UserId2 == userId1));

			if (existingFriendship != null)
			{
				throw new InvalidOperationException("Friendship already exists.");
			}

			var newFriendship = new Friendship
			{
				UserId1 = userId1,
				UserId2 = userId2,
				FriendDate = DateTime.Now,
				Accepted = false
			};

			db.Friendships.Add(newFriendship);
			db.SaveChanges();
		}

		public void AcceptFriendship(string userId1, string userId2)
		{
			var friendship = db.Friendships
								 .FirstOrDefault(f => (f.UserId1 == userId1 && f.UserId2 == userId2) || (f.UserId1 == userId2 && f.UserId2 == userId1));

			if (friendship == null)
			{
				throw new InvalidOperationException("Friendship request not found.");
			}

			friendship.Accepted = true;
			friendship.FriendDate = DateTime.Now;

			db.SaveChanges();
		}

		public Profile GetProfile(string userId)
		{
			return db.Profiles.Where(x => x.UserId == userId).FirstOrDefault();
		}

        public ProfileData GetProfileData(string userId)
        {
            // Retrieve profile information including the username
            var profileData = (from p in db.Profiles
                               join u in db.Users on p.UserId equals u.Id
                               where p.UserId == userId
                               select new ProfileData
                               {
                                   UserId = p.UserId,
                                   Username = u.UserName,
                                   Image = p.Image,
                                   Bio = p.Bio,
                                   Games = GetGameDataByUserId(userId).ToList(),
                                   FavoriteGame = GetFavoriteGameByUserId(userId),
                                   Birthday = p.Birthday,
                                   Location = p.Location,
                                   Friends = GetFriendsByUserId(userId).Select(x => x.UserId).ToList()
                               }).FirstOrDefault();

            return profileData;
        }

        private GameData GetFavoriteGameByUserId(string userId)
        {
            var favoriteGameId = db.Profiles.Where(p => p.UserId == userId).Select(p => p.FavoriteGame).FirstOrDefault();
            if (favoriteGameId.HasValue)
            {
                var game = GamesAPI.GetGame((int)favoriteGameId).Result;
                if (game != null)
                {
                    return new GameData(game);
                }
            }
            return null;
        }

        public List<ProfileData> SearchProfilesByUsername(string search)
        {

            var profiles = (from p in db.Profiles
                            join u in db.Users on p.UserId equals u.Id
                            select new { Profile = p, User = u }).ToList();

            var profileData = profiles
                .Where(x => x.User.UserName.ToLower().Contains(search.ToLower()))
                .Select(x => new ProfileData
                {
                    UserId = x.Profile.UserId,
                    Username = x.User.UserName,
                    Image = x.Profile.Image,
                    Bio = x.Profile.Bio,
                    Games = GetGameDataByUserId(x.Profile.UserId).ToList(),
                    FavoriteGame = GetFavoriteGameByUserId(x.Profile.UserId),
                    Birthday = x.Profile.Birthday,
                    Location = x.Profile.Location,
                    Friends = GetFriendsByUserId(x.Profile.UserId).Select(f => f.UserId).ToList()
                })
                .ToList();

            return profileData;
        }

        public bool IsGameInLibrary(string userId, long gameId)
        {
            return db.UserGames.Any(x => x.UserId == userId && x.GameId == gameId);
        }

        public void UpdateProfile(Profile profile)
        {
            db.Profiles.Update(profile);
            db.SaveChanges();
        }
        public void SetFavoriteGame(string userId, long gameId)
        {
            var model = GetProfile(userId);
            model.FavoriteGame = gameId;
            db.Profiles.Update(model);
            db.SaveChanges();
        }
    }
}
