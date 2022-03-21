﻿namespace KamaVerification.Data.Models
{
    public class CustomerEmailConfig
    {
        public int CustomerEmailConfigId { get; set; }
        public Guid PublicKey { get; set; }
        public int CustomerId { get; set; }
        public string Subject { get; set; }
        public string FromEmail { get; set; }
        public string FromName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}