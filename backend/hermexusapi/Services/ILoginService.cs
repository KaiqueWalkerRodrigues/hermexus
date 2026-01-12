using hermexusapi.DTO.V1;

namespace hermexusapi.Services
{
    public interface ILoginService
    {
        TokenDTO? ValidateCredentials(SignInDTO user);
        TokenDTO? ValidateCredentials(TokenDTO token);
        bool RevokeToken(string username);
        AccountCredentialsDTO Create(AccountCredentialsDTO user);
    }
}
