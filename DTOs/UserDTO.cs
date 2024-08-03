namespace TE_trsprt_remake.DTOs
{
    public class UserDTO
    {
        public string TE_Id { get; set; }
        public string FullName { get; set; }

        public string Email { get; set; }

        public string SvEmail { get; set; }

        public string AccountStatus { get; set; }

        public string Role { get; set; }
        public string Title { get; set; }
        public string Password { get; set; }
        public DateTime CreatedAt { get; set; }
        public int PlantId { get; set; }
        public int DepartementId { get; set; }
        public string Address { get; set; }

    }
}

