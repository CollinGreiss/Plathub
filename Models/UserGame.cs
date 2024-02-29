using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Plathub.Models
{
    public class UserGame
    {
        public UserGame() { }
        public UserGame(string userId, long gameId) { UserId = userId; GameId = gameId; }
        [Key, Column(Order = 0)]
        public string UserId { get; set; }
        [Key, Column(Order = 1)]
        public long GameId { get; set; }
    }
}
