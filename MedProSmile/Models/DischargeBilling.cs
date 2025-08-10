using System.Security.Principal;

namespace MedProSmile.Models
{
    public class DischargeBilling
    {
        public int HospitalId { get; set; }
        public int PatientId { get; set; }
        public int SummaryId { get; set; }

        public decimal RoomCharges { get; set; }
        public decimal ConsultationFees { get; set; }
        public decimal MedicineCharges { get; set; }
        public decimal LabCharges { get; set; }
        public decimal OtherCharges { get; set; }

        public decimal Discount { get; set; }

        public string PaymentStatus { get; set; } = string.Empty;

        public int CreatedBy { get; set; }

    }

    public class DischargeBillingUpdate
    {
        public int DischargeBillingId { get; set; }
        public int HospitalId { get; set; }
        public int PatientId { get; set; }
        public int SummaryId { get; set; }

        public decimal RoomCharges { get; set; }
        public decimal ConsultationFees { get; set; }
        public decimal MedicineCharges { get; set; }
        public decimal LabCharges { get; set; }
        public decimal OtherCharges { get; set; }

        public decimal Discount { get; set; }

        public string PaymentStatus { get; set; } = string.Empty;

        public int UpdatedBy { get; set; }

    }

    public class DischargeBillingDelete
    {
       
        public int DischargeBillingId { get; set; }
        public int UpdatedBy { get; set; }
    }
}
