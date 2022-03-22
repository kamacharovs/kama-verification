namespace KamaVerification.Data.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public Guid PublicKey { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; } = false;
        public virtual CustomerEmailConfig EmailConfig { get; set; }
    }
}
