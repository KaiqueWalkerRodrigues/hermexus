using hermexusapi.DTO.V1;
using hermexusapi.Models;

namespace hermexusapi.Services
{
    public interface ILoginService
    {
        TokenDTO? ValidateCredentials(SignInDTO user);
        TokenDTO? ValidateCredentials(TokenDTO token);
        bool RevokeToken(string username);
    }
}
