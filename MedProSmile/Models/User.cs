namespace MedProSmile.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public int HospitalId { get; set; }
        public int RoleId { get; set; }
        public string Role { get; set; } = string.Empty;
        public int? DoctorId { get; set; }
    }
}
