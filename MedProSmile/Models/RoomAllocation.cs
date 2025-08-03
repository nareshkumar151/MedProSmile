namespace MedProSmile.Models
{
    public class RoomAllocation
    {
        public int HospitalId { get; set; }

        public int PatientId { get; set; }

        public int DepartmentId { get; set; }

        public string RoomNumber { get; set; } = string.Empty;

        public string BedNumber { get; set; } = string.Empty;

        public DateTime AdmissionDate { get; set; }

        public DateTime? DischargeDate { get; set; }

        public bool Status { get; set; } = true;

        public int CreatedBy { get; set; }

    }
    public class RoomAllocationUpdate
    {

        public int RoomId { get; set; }

        public int HospitalId { get; set; }

        public int PatientId { get; set; }

        public int DepartmentId { get; set; }

        public string RoomNumber { get; set; } = string.Empty;

        public string BedNumber { get; set; } = string.Empty;

        public DateTime AdmissionDate { get; set; }

        public DateTime? DischargeDate { get; set; }

        public bool Status { get; set; } = true;

        public int UpdatedBy { get; set; }

    }

    public class RoomAllocationDelete
    {
        public int RoomId { get; set; }

        public int UpdatedBy { get; set; }

    }
}
