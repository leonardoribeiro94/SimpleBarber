namespace SimpleBarber.Api.Domain
{
    public class Appointments
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public DateTime DateAppointment { get; set; }
        public bool Status { get; set; }
    }
}
