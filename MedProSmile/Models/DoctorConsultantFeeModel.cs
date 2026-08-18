namespace MedProSmile.Models
{
    public class DoctorConsultantFeeModel
    {
        public int ConsultantFeeId { get; set; }
        public int DoctorId { get; set; }
        public string? FullName { get; set; }
        public int ConsultationTypeId { get; set; }
        public string? ConsultationType { get; set; }
        public decimal FeeAmount { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
