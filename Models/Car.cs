using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TE_trsprt_remake.Models
{
    public class Car
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Brand { get; set; }

        public string Type { get; set; }

        public string Transmission { get; set; }

        public string LicensePlate { get; set; }

        public string Condition { get; set; }

        public virtual ICollection<Request> Requests { get; set; }

        public int PlantId { get; set; }

        [ForeignKey("PlantId")]
        public virtual Plant Plant { get; set; }

    }
}
