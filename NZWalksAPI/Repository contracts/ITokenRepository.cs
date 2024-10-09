using Microsoft.AspNetCore.Identity;

namespace NZWalksAPI.Repository_contracts
{
    public interface ITokenRepository
    {
        string CreateJWTToken(IdentityUser user, List<string> roles);
    }
}
