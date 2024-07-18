using System.ComponentModel.DataAnnotations;

namespace TE_trsprt_remake.Models
{
    public class Departement
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
