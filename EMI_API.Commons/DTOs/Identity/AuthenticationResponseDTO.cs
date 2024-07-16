namespace EMI_API.Commons.DTOs.Identity
{
    public class AuthenticationResponseDTO
    {
        public string Token { get; set; } = null!;
        public DateTime Expiration { get; set; }
    }
}
