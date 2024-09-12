using System.ComponentModel.DataAnnotations;

namespace TE_trsprt_remake.DTOs
{
    public class RegisterDTO
    {
        public string TE_Id { get; set; }
        public string FullName { get; set; }
        public string Title { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int DepartementId { get; set; }
        public List<int> PlantIds { get; set; }
    }
}
