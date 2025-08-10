namespace MedProSmile.Models
{
    public class Staff
    {
        public int HospitalId { get; set; }
        public string FullName { get; set; }
        public int DepartmentId { get; set; }
        public int RoleId { get; set; }
        public DateTime JoinDate { get; set; }
        public string Gender { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int CreatedBy { get; set; }

    }
    public class StaffUpdate
    {
        public int StaffId { get; set; }
        public int HospitalId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public int DepartmentId { get; set; }
        public int RoleId { get; set; }
        public DateTime JoinDate { get; set; }
        public string Gender { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int UpdatedBy { get; set; }

    }
    public class StaffDelete
    {
        public int StaffId { get; set; }
        public int UpdatedBy { get; set; }
    }
}
