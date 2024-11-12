namespace SibersProjectBusiness.DTOs.Auth
{
    public class AuthResponseDto
    {
        public long Id { get; set; }
        public string AccessToken { get; set; } = null!;
        public string Role { get; set; } = null!;
    }
}
