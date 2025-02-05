using System.ComponentModel.DataAnnotations.Schema;

namespace Sukalibur.Graph.Users
{
    [Table("follows")]
    public class Follow
    {
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Column("follower_id")]
        public int FollowerId { get; set; }
        [Column("following_id")]
        public int FollowingId { get; set; }
        public User Follower { get; set; } = null!;
        public User Following { get; set; } = null!;
    }
}
