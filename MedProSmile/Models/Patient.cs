namespace MedProSmile.Models
{
    public class Patient
    {
        public int HospitalId { get; set; }
        public string FullName { get; set; }
        public DateTime? DOB { get; set; }
        public string? Gender { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public int DepartmentId { get; set; }
        public bool Status { get; set; } = true;
        public int CreatedBy { get; set; }
    }
    public class PatientUpdateDto
    {
        public int PatientId { get; set; }
        public int HospitalId { get; set; }
        public string FullName { get; set; }
        public DateTime? DOB { get; set; }
        public string? Gender { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public int DepartmentId { get; set; }
        public bool Status { get; set; } = true;
        public int UpdatedBy { get; set; }
    }
    public class PatientDeleteDto
    {
        public int PatientId { get; set; }
        public int UpdatedBy { get; set; }
    }


}
