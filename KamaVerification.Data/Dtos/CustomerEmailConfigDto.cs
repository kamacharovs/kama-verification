namespace KamaVerification.Data.Dtos
{
    public class CustomerEmailConfigDto
    {
        public string Subject { get; set; }
        public string FromEmail { get; set; }
        public string FromName { get; set; }
        public int? ExpirationInMinutes { get; set; }
    }
}
