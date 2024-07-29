using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TE_trsprt_remake.DTOs
{
    public class ApprovalDTO
    {
        public int Id { get; set; }

        public int ApproverId { get; set; }

        public int RequestId { get; set; }

        public string Position { get; set; }

        public string Status { get; set; }

        public string Comment { get; set; }

        public DateTime CreatedAt { get; set; }

    }
}
