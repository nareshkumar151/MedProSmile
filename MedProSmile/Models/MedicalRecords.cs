namespace MedProSmile.Models
{
    public class MedicalRecords
    {
        public int HospitalId { get; set; }
        public int PatientId { get; set; }
        public int DepartmentId { get; set; }
        public string Diagnosis { get; set; }
        public string Prescription { get; set; }
        public bool Status { get; set; }
        public int CreatedBy { get; set; }
    }

    public class MedicalRecordUpdate
    {
        public int RecordId { get; set; }
        public int HospitalId { get; set; }
        public int PatientId { get; set; }
        public int DepartmentId { get; set; }
        public string Diagnosis { get; set; }
        public string Prescription { get; set; }
        public bool Status { get; set; }
        public int UpdatedBy { get; set; }
    }

    public class MedicalRecordDelete
    {
        public int RecordId { get; set; }
        public int UpdatedBy { get; set; }
    }
}
