namespace MedProSmile.Models
{
    public class PatientMedicine
    {
        public int HospitalId { get; set; }

        public int PatientId { get; set; }

        public int DoctorId { get; set; }

        public string MedicineName { get; set; } = string.Empty;

        public string Dosage { get; set; } = string.Empty;

        public string Frequency { get; set; } = string.Empty;

        public string Duration { get; set; } = string.Empty;

        public string Notes { get; set; } = string.Empty;

        public int CreatedBy { get; set; }

    }

    public class PatientMedicineUpdate
    {
        public int MedicineId { get; set; }

        public int HospitalId { get; set; }

        public int PatientId { get; set; }

        public int DoctorId { get; set; }

        public string MedicineName { get; set; } = string.Empty;

        public string Dosage { get; set; } = string.Empty;

        public string Frequency { get; set; } = string.Empty;

        public string Duration { get; set; } = string.Empty;

        public string Notes { get; set; } = string.Empty;

        public int UpdatedBy { get; set; }



    }

    public class PatientMedicineDelete
    {
        public int MedicineId { get; set; }

        public int UpdatedBy { get; set; }

    }

}
