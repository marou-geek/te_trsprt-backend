using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TE_trsprt_remake.Models
{
    public class UserPlant
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        
        public User User { get; set; }

        [ForeignKey("Plant")]
        public int PlantId { get; set; }
        
        public Plant Plant { get; set; }
    }
}
