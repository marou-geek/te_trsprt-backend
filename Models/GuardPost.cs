using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TE_trsprt_remake.Models
{
    public class GuardPost
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Request")]
        public int RequestId { get; set; }

        public float? PredKms { get; set; }
        public float? RealKms { get; set; }
        public int? NbrPersons { get; set; }
        public string? FuelLevel { get; set; }
        public bool? RegCard { get; set; }
        public bool? Insurance { get; set; }
        public bool? CarHealthCert { get; set; }
        public bool? Vignette { get; set; }
        public bool ?FuelCard { get; set; }
        public string? Accessories { get; set; }
        public string? ExCondition { get; set; }  
        public string? IntCondition { get; set; }  
        public string? MechCondition { get; set; }
        public string Type { get; set; }
        public DateTime CreatedAt { get; set; }
        public virtual Request Request { get; set; }
    }
}
