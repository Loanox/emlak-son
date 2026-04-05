using emlak_son.Entities;

namespace emlak_son.Services;

public interface ITokenService
{
    Task<string> CreateTokenAsync(AppUser user);
}
