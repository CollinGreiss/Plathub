using IGDB.Models;
using Plathub.Interfaces;
using Plathub.Models;
using Plathub.APIs;
using System;

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

        public void RemoveGameFromLibrary(UserGame model)
        {
            db.UserGames.Remove(model);
            db.SaveChanges();
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
    }
}
