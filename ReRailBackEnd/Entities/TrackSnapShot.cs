using System.Buffers.Text;
using System.ComponentModel.DataAnnotations;

namespace ReRailBackEnd.Entities
{
    public class TrackSnapShot
    {
        [Key]
        public int Id { get; set; }

        public byte[] Image { get; set; } = null!;

        public string Location { get; set; } = null!;

        public DateTime CreationDate { get; set; }

        public bool Treated { get; set; }
    }
}
