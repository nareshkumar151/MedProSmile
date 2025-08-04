namespace MedProSmile.Models
{
    public class DischargeSummary
    {
        public int HospitalId { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public int DepartmentId { get; set; }
        public DateTime AdmissionDate { get; set; }
        public DateTime DischargeDate { get; set; }
        public string Diagnosis { get; set; } = string.Empty;
        public string TreatmentGiven { get; set; } = string.Empty;
        public string FollowUpAdvice { get; set; } = string.Empty;
        public int CreatedBy { get; set; }

    }
    public class DischargeSummaryUpdate
    {
        public int SummaryId { get; set; }
        public int HospitalId { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public int DepartmentId { get; set; }
        public DateTime AdmissionDate { get; set; }
        public DateTime DischargeDate { get; set; }
        public string Diagnosis { get; set; } = string.Empty;
        public string TreatmentGiven { get; set; } = string.Empty;
        public string FollowUpAdvice { get; set; } = string.Empty;
        public int UpdatedBy { get; set; }

    }

    public class DischargeSummaryDelete
    {
        public int SummaryId { get; set; }
        public int UpdatedBy { get; set; }
    }
}
