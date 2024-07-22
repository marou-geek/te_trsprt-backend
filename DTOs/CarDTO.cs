using System.ComponentModel.DataAnnotations;

namespace TE_trsprt_remake.DTOs
{
    public class CarDTO
    {
        public int Id { get; set; }

        public string Brand { get; set; }

        public string Type { get; set; }

        public string LicensePlate { get; set; }

        public string PlantId { get; set; }

        public string Condition { get; set; }
    }
}
