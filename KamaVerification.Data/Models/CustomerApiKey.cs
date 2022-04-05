namespace KamaVerification.Data.Models
{
    public class CustomerApiKey
    {
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        public Guid PublicKey { get; set; } = Guid.NewGuid();
        public string ApiKey { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public bool IsEnabled { get; set; } = true;
    }
}
