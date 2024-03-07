using System.ComponentModel.DataAnnotations;

namespace Plathub.Models
{
    public class Profile
    {
        public Profile() { }
        [Key]
        public string UserId { get; set; }
        public string? Bio { get; set; }
        public string? Image { get; set; }
        public DateTime? Birthday { get; set; }
        public string? Location { get; set; }
        public long? FavoriteGame {  get; set; }
    }
}
