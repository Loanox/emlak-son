namespace emlak_son.DTOs.Auth;

public class AuthResponseDto
{
    public string Token { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public IEnumerable<string> Roles { get; set; } = Enumerable.Empty<string>();
}
