namespace MedProSmile.Models
{
    public class Users
    {
        public int HospitalId { get; set; }

        public string Username { get; set; } = string.Empty;

        public string PasswordHash { get; set; } = string.Empty;

        public int RoleId { get; set; }

        public bool Status { get; set; } = true;

        public int CreatedBy { get; set; }

    }

    public class UsersUpdate
    {
        public int UserId { get; set; }

        public int HospitalId { get; set; }

        public string Username { get; set; } = string.Empty;

        public string PasswordHash { get; set; } = string.Empty;

        public int RoleId { get; set; }

        public bool Status { get; set; } = true;

        public int UpdatedBy { get; set; }

    }

    public class UsersDelete
    {
        public int UserId { get; set; }

        public int UpdatedBy { get; set; }

    }
}
