namespace KamaVerification.Data.Dtos
{
    public class CustomerDto
    {
        public string Name { get; set; }
        public bool GenerateApiKey { get; set; } = true;
        public CustomerEmailConfigDto EmailConfig { get; set; }
    }
}
