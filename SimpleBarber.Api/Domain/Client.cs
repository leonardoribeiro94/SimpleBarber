using SimpleBarber.Api.DTO;

namespace SimpleBarber.Api.Domain
{
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int UserId { get; set; }
        public User? User { get; set; }  
        public DateOnly BirthDate { get; set; } 
        public ICollection<Appointment> Appointments { get; } = new List<Appointment>().AsReadOnly();

        public void Update(string name, string phone, string email, DateOnly birthDate)
        {
           Name = name ?? throw new ArgumentNullException(nameof(name));
           Phone = phone ?? throw new ArgumentNullException(nameof(phone));
           Email = email ?? throw new ArgumentNullException(nameof(email));
           BirthDate = birthDate;
        }
    }
}
