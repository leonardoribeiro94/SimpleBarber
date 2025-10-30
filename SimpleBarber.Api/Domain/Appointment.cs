namespace SimpleBarber.Api.Domain
{
    public class Appointment
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public Client Client { get; set; }
        public DateTime DateAppointment { get; set; }
        public bool Status { get; set; }
        public ICollection<Service> Services { get; } = new List<Service>().AsReadOnly();

    }
}
