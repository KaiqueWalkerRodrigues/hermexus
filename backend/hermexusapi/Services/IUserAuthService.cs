using hermexusapi.DTO.V1;
using hermexusapi.Models;

namespace hermexusapi.Services
{
    public interface IUserAuthService
    {
        User? FindByUsername(string username);
        User Create(AccountCredentialsDTO dto);
        bool RevokeToken(string username);
        User Update(User user);
    }
}
