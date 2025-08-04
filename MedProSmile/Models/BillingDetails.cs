namespace MedProSmile.Models
{
    public class BillingDetails
    {
        public int HospitalId { get; set; }
        public int BillingId { get; set; }
        public string ItemDescription { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public int CreatedBy { get; set; }

    }
    public class BillingDetailsUpdate
    {
        public int BillingDetailId { get; set; }
        public int HospitalId { get; set; }
        public int BillingId { get; set; }
        public string ItemDescription { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public int UpdatedBy { get; set; }

    }
    public class BillingDetailsDelete
    {
        public int BillingDetailId { get; set; }
        public int UpdatedBy { get; set; }
    }
}
