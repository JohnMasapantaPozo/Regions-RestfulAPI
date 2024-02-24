using Microsoft.AspNetCore.Identity;

namespace RestfulDEMO.API.Repositories
{
    public interface ITokenRepository
    {
        string CreateJwtToken(IdentityUser user, List<string> roles);
    }
}
