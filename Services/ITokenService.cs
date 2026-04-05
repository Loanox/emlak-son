using emlak_son.Models;

namespace emlak_son.Services;

public interface ITokenService
{
    Task<string> CreateTokenAsync(AppUser user);
}
