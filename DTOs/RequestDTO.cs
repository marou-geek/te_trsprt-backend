using System.ComponentModel.DataAnnotations.Schema;

namespace TE_trsprt_remake.DTOs
{
    public class RequestDTO
    {

        public int Id { get; set; }

        public int RequesterId { get; set; }
        public int CarId { get; set; }

        public DateOnly FromDate { get; set; }

        public DateOnly ToDate { get; set; }

        public string Raison { get; set; }

        public string Status { get; set; }

        public string FromDestination { get; set; }

        public string ToDestination { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
