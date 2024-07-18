using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TE_trsprt_remake.Models
{
    public class Plant
    {
        [Key]
        public int Id { get; set; }
        public string Location { get; set; }
        public int SiteManagerId { get; set; }


        [ForeignKey("SAP")]public int SAPId { get; set; }
        [ForeignKey("Building")] public int BuildingId { get; set; }
        public virtual SAP SAP { get; set; }
        public virtual Building Building { get; set; }

        public virtual ICollection<User> Users { get; set; }




    }
}
