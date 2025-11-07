namespace SimpleBarber.Api.Domain
{
    public class Service
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int EstimateTime { get; set; }
        public decimal Price { get; set; }
        
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
        
        public void Update(string name, string description, int estimateTime, decimal price)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Description = description ?? throw new ArgumentNullException(nameof(description));
            EstimateTime = estimateTime;
            Price = price;
        }
    }
}
