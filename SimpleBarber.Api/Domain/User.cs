namespace SimpleBarber.Api.Domain
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Role { get; set; } = "customer"; // admin, barber, customer
        
        public Customer? Customer { get; set; }
    }
}
