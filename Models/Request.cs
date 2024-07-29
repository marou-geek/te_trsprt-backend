using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TE_trsprt_remake.Models
{
    public class Request
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("User")]
        public int RequesterId { get; set; }

        [ForeignKey("Car")]
        public int CarId { get; set; }

        public DateOnly FromDate { get; set; }

        public DateOnly ToDate { get; set; }

        public string Raison { get; set; }

        public string Status { get; set; }

        public string FromDestination { get; set; }

        public string ToDestination { get; set; }

        public DateTime CreatedAt { get; set; }

        public virtual User User { get; set; }

        public virtual Car Car { get; set; }

        public virtual ICollection<Approval> Approvals { get; set; }



    }
}
