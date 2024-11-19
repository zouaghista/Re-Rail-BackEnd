using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReRailBackEnd.Entities
{
    public class TrackPoint
    {
        [Key]
        public int Id { get; set; }

        public string Prediction { get; set; } = null!;

        [Required]
        public TrackSnapShot trackSnapShot { get; set; } = null!;

        [ForeignKey("trackSnapShot")]
        public int trackSnapShotId { get; set; }
    }
}
