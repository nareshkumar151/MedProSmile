namespace MedProSmile.Models
{
    
    public class Appointment
    {
        public int HospitalId { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public TimeSpan AppointmentTime { get; set; }
        public string? Reason { get; set; }
        public string AppointmentStatus { get; set; }
        public bool Status { get; set; }
        public int CreatedBy { get; set; }
    }
    public class AppointmentUpdate
    {
        public int AppointmentId { get; set; }
        public int HospitalId { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public TimeSpan AppointmentTime { get; set; }
        public string? Reason { get; set; }
        public string AppointmentStatus { get; set; }
        public bool Status { get; set; }
        public int UpdatedBy { get; set; }
    }
    public class AppointmentDelete
    {
        public int AppointmentId { get; set; }
        public int UpdatedBy { get; set; }
    }
}
