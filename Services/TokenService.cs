using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using emlak_son.Entities;
using emlak_son.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace emlak_son.Services;

public class TokenService : ITokenService
{
    private readonly JwtSettings _jwtSettings;
    private readonly UserManager<AppUser> _userManager;

    public TokenService(IOptions<JwtSettings> jwtSettings, UserManager<AppUser> userManager)
    {
        _jwtSettings = jwtSettings.Value;
        _userManager = userManager;
    }

    public async Task<string> CreateTokenAsync(AppUser user)
    {
        var roles = await _userManager.GetRolesAsync(user);

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Email, user.Email ?? string.Empty)
        };

        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpirationInMinutes),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
