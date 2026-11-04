namespace Project.Api.DTOs
{
    public class RegisterPackageRequest
    {
        public string Number { get; set; } = string.Empty;
        public string Zone { get; set; } = string.Empty;
        public string Cell { get; set; } = string.Empty;
    }
}
