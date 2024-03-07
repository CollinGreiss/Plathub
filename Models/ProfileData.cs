namespace Plathub.Models
{
    public class ProfileData
    {
        public string UserId { get; set; }
        public string Username { get; set; }
        public string Image { get; set; }
        public string? Bio {  get; set; }
        public List<GameData> Games { get; set; }
        public GameData FavoriteGame { get; set; }
        public DateTime? Birthday { get; set; }
        public string? Location { get; set; }
        public List<string> Friends { get; set; }
    }
}
