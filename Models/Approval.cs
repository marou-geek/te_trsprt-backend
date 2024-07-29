using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TE_trsprt_remake.Models
{
    public class Approval
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("User")]
        public int ApproverId { get; set; }

        [ForeignKey("Request")]
        public int RequestId { get; set; }

        public string Position { get; set; }

        public string Status { get; set; }

        public string Comment { get; set; }

        public DateTime CreatedAt { get; set; }

        public virtual User User { get; set; }

        public virtual Request Request { get; set; }

    }
}
