using System.ComponentModel.DataAnnotations;

namespace Plathub.Models
{
    public class Game
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        
    }
}
