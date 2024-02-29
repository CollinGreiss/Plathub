using Plathub.Interfaces;
using Plathub.Models;
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

        public IEnumerable<long> GetGameIdsByUserId(string userId)
        {
            return db.UserGames.Where(x => x.UserId == userId).Select(x => x.GameId);
        }
    }
}
