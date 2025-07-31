namespace MedProSmile.Models
{
    public class Doctor
    {
        public string FullName { get; set; }

        public int HospitalId { get; set; }

        public int DepartmentId { get; set; }

        public string? Qualification { get; set; }

        public string? Specialization { get; set; }

        public string? Phone { get; set; }

        public string? Email { get; set; }

        public string? Gender { get; set; }

        public string? ProfilePhotoUrl { get; set; }

        public TimeSpan? AvailableFrom { get; set; }

        public TimeSpan? AvailableTo { get; set; }

        public bool Status { get; set; } = true;

        public int CreatedBy { get; set; }
    }

    public class DoctorUpdateDto
    {
        public int DoctorId { get; set; }

        public string FullName { get; set; }

        public int HospitalId { get; set; }

        public int DepartmentId { get; set; }

        public string? Qualification { get; set; }

        public string? Specialization { get; set; }

        public string? Phone { get; set; }

        public string? Email { get; set; }

        public string? Gender { get; set; }

        public string? ProfilePhotoUrl { get; set; }

        public TimeSpan? AvailableFrom { get; set; }

        public TimeSpan? AvailableTo { get; set; }

        public bool Status { get; set; } = true;

        public int UpdatedBy { get; set; }
    }

    public class DoctorDeleteDto
    {
        public int DoctorId { get; set; }

        public int UpdatedBy { get; set; }
    }

}
