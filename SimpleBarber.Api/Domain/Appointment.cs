namespace SimpleBarber.Api.Domain
{
    public class Appointment
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public DateTime DateAppointment { get; set; }
        public bool Status { get; set; }
        public string? Notes { get; set; }
        public ICollection<Service> Services { get; } = new List<Service>().AsReadOnly();

    }
}
