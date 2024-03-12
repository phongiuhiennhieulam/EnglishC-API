using EnglishCenter.Request;
using System.Security.Claims;

namespace EnglishCenter.Token
{
    public interface IManageToken
    {
        public String generateToken(LoginRequest userRequest);
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
