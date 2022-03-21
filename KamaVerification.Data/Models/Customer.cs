using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public CustomerEmailConfig EmailConfig { get; set; }
    }
}
