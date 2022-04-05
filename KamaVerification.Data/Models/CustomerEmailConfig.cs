namespace KamaVerification.Data.Models
{
    public class CustomerEmailConfig
    {
        public int CustomerId { get; set; }
        public Guid PublicKey { get; set; } = Guid.NewGuid();
        public string Subject { get; set; }
        public string FromEmail { get; set; }
        public string FromName { get; set; }
        public int ExpirationInMinutes { get; set; } = 60;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
