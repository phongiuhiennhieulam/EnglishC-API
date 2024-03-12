using EnglishCenter.Repository;
using EnglishCenter.Request;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EnglishCenter.Token
{
    public class ManageToken : IManageToken
    {
        private readonly IAccountRepository accountRepository;
        private readonly IConfiguration _configuration;

        public ManageToken(IAccountRepository _accountRepository, IConfiguration configuration)
        {
            accountRepository = _accountRepository;
            _configuration = configuration;
        }
        public String generateToken(LoginRequest userRequest)
        {

            
            var account = accountRepository.GetAccountByUsernamePassword(userRequest.email,userRequest.password);
            var jwtToken = new JwtSecurityTokenHandler();

            var secretKeyEncrypt = Encoding.UTF8.GetBytes(_configuration.GetSection("Config").GetSection("SecretKey").Value);
            String id = account.Id.ToString();
            String email = account.Email;
            String role = account.Role.Name;
            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("ID",id),
                    new Claim(ClaimTypes.Name,email),
                    new Claim("TokenId",Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.Role, role)
        }),
                Expires = DateTime.UtcNow.AddDays(40),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyEncrypt),
                SecurityAlgorithms.HmacSha256Signature)
            };
            var token = jwtToken.CreateToken(tokenDescription);
            return jwtToken.WriteToken(token);
        }
        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("Config").GetSection("SecretKey").Value)),
                ValidateLifetime = false
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");
            return principal;
        }
    }
}
