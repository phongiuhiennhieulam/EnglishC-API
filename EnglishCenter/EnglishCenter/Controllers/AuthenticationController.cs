using EnglishCenter.Repository;
using EnglishCenter.Request;
using EnglishCenter.Token;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EnglishCenter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAccountRepository accountRepository;
        private readonly IManageToken _manageToken;
        public AuthenticationController(IAccountRepository _accountRepository, IManageToken manageToken)
        {
            accountRepository = _accountRepository;
            _manageToken = manageToken;
        }
        [HttpPost("Login")]
        public IActionResult LoginTeacher(LoginRequest userRequest)
        {
            if (accountRepository.GetAccountByUsernamePassword(userRequest.email, userRequest.password) == null)
            {
                return Ok("Login Fail");
            }
            else
            {
                var user = accountRepository.GetAccountByUsernamePassword(userRequest.email, userRequest.password);
                var accessToken = _manageToken.generateToken(userRequest);
                

                return Ok(accessToken);
            }

        }
    }
}
