using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public virtual ICollection<User> Users { get; set; }

    }
}
