using System.ComponentModel.DataAnnotations;

namespace TE_trsprt_remake.Models
{
    public class Building
    {
        [Key]public int BuildingId { get; set; }
        public string BuildingName { get; set; }

        public virtual ICollection<Plant> Plants { get; set; }

    }
}
