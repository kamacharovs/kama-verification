namespace KamaVerification.Data.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public Guid PublicKey { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public bool? IsDeleted { get; set; } = false;
        public virtual CustomerApiKey ApiKey { get; set; }
        public virtual CustomerEmailConfig EmailConfig { get; set; }
    }
}
