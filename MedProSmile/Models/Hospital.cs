namespace MedProSmile.Models
{
    public class Hospital
    {
        public string? HospitalName { get; set; }

        public string? ContactNumber { get; set; }

        public string? Address { get; set; }

        public int CityId { get; set; }

        public int StateId { get; set; }

        public int? PinCode { get; set; }

        public bool Status { get; set; } = true;

        public int CreatedBy { get; set; }
    }

    public class HospitalUpdateDto
    {
        public int HospitalId { get; set; }

        public string? HospitalName { get; set; }

        public string? ContactNumber { get; set; }

        public string? Address { get; set; }

        public int CityId { get; set; }

        public int StateId { get; set; }

        public int? PinCode { get; set; }

        public bool Status { get; set; } = true;

        public int UpdatedBy { get; set; }
    }

    public class HospitalDeleteDto
    {
        public int HospitalId { get; set; }
        public int UpdatedBy { get; set; }
    }


}
