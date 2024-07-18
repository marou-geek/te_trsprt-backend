using System.ComponentModel.DataAnnotations;

namespace TE_trsprt_remake.Models
{
    public class SAP
    {
        [Key]public int SAPId { get; set; }
        public string SAPName { get; set;}

        public virtual ICollection<Plant> Plants { get; set; }

    }
}
