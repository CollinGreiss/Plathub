using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Plathub.Models
{
    public class Friendship
    {
        [Key, Column(Order = 0)]
        public string UserId1 { get; set; }
        [Key, Column(Order = 1)]
        public string UserId2 { get; set; }
        public DateTime FriendDate { get; set; }
        public bool Accepted { get; set; }
    }
}
