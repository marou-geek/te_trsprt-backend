using System.ComponentModel.DataAnnotations;

namespace TE_trsprt_remake.Models
{
    public class Car
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Brand { get; set; }

        public string Type { get; set; }

        public string LicensePlate { get; set; }

        public string Condition { get; set; }


    }
}
