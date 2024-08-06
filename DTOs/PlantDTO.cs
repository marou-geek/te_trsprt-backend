using System.ComponentModel.DataAnnotations.Schema;
using TE_trsprt_remake.Models;

namespace TE_trsprt_remake.DTOs
{
    public class PlantDTO
    {
        public string Location { get; set; }
        public string SAPId { get; set; }
        public string BuildingId { get; set; }
        public string SiteManagerEmail { get; set; }
    }
}
