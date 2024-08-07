using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using TE_trsprt_remake.Models;

namespace TE_trsprt_remake.DTOs
{
    public class UserPlantDTO
    {
  
            public int Id { get; set; }

            public int UserId { get; set; }

            public int PlantId { get; set; }

    }   
}
