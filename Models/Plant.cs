using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TE_trsprt_remake.Models
{
    public class Plant
    {
        [Key]
        public int Id { get; set; }

        public string Location { get; set; }

        public string SiteManagerEmail { get; set; }

        public string SAPId { get; set; }

        public string BuildingId { get; set; }

        public virtual ICollection<UserPlant> UserPlants { get; set; } = new HashSet<UserPlant>();

        public virtual ICollection<Car> Cars { get; set; } = new HashSet<Car>();


    }
}
