using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TE_trsprt_remake.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string TE_Id { get; set; }

        public string FullName { get; set; }

        public string Title { get; set; }

        public string SvEmail { get; set; }

        [Required]
        public string Email { get; set; }

        [ForeignKey("Departement")]
        public int? DepartementId { get; set; }

        public string Password { get; set; }

        public string AccountStatus { get; set; }

        public string Role { get; set; }

        public string? PasswordResetToken { get; set; }

        public DateTime CreatedAt { get; set; }

        public virtual Departement Departement { get; set; }

        public virtual ICollection<UserPlant> UserPlants { get; set; } = new HashSet<UserPlant>();

        public virtual ICollection<Request> Requests { get; set; } = new HashSet<Request>();
    }
}
