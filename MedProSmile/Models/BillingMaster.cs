namespace MedProSmile.Models
{
    public class BillingMaster
    {
        public int HospitalId { get; set; }
        public int PatientId { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal DiscountAmount { get; set; } = 0;
        public decimal PaidAmount { get; set; } = 0;
        public string PaymentMode { get; set; } = string.Empty;
        public string PaymentStatus { get; set; } = "Pending";
        public int CreatedBy { get; set; }

    }

    public class BillingMasterUpdate
    {
        public int BillingId { get; set; }
        public int HospitalId { get; set; }
        public int PatientId { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal PaidAmount { get; set; }
        public string PaymentMode { get; set; } = string.Empty;
        public string PaymentStatus { get; set; } = string.Empty;
        public int UpdatedBy { get; set; }

    }

    public class BillingMasterDelete
    {
        public int BillingId { get; set; }
        public int UpdatedBy { get; set; }

    }
}
