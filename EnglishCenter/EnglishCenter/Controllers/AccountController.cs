using EnglishCenter.Repository;
using EnglishCenter.Request;
using EnglishCenter.Validate;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EnglishCenter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository accountRepository;
        public AccountController(IAccountRepository _accountRepository)
        {
            accountRepository = _accountRepository;
        }

        [HttpGet("ManageAccountAdmin")]
        public IActionResult Get()
        {
            try
            {

                return Ok(accountRepository.getAllAccount());
            }
            catch (Exception ex)
            {

                return BadRequest(null);
            }
        }
        [HttpGet("{email}")]
        public IActionResult Profile(String email)
        {
            try
            {

                return Ok(accountRepository.GetAccountByEmail(email));
            }
            catch (Exception ex)
            {

                return BadRequest(null);
            }
        }

        [HttpPut("ActionAccount")]
        public void ActionAccount([FromBody] AccountManage request)
        {
            try
            {
                accountRepository.ActionAccount(request.account_id, request.status);
            }
            catch (Exception ex)
            {

            }
        }

        [HttpPost("AddAccount")]
        public IActionResult AddAccount([FromBody] AddAccountRequest request)
        {
            
            try
            {
                List<string> errors = new AccountValidate(accountRepository).ValidateAddAccountRequest(request);
                if(errors.Count > 0)
                {
                    return BadRequest(errors);
                }
                else
                {
                    accountRepository.AddAccount(request);
                    return Ok(errors);
                }
                
            }
            catch (Exception ex)
            {
                List<string> errors = new List<string>();
                errors.Add(ex.Message);
                return BadRequest(errors);
            }
        }


        [HttpPut("UpdateProfile")]
        public IActionResult UpdateProfile([FromBody] UpdateProfileRequest request)
        {

            try
            {
                List<string> errors = new AccountValidate(accountRepository).ValidateUpdateProfileRequest(request);
                if (errors.Count > 0)
                {
                    return BadRequest(errors);
                }
                else
                {
                    accountRepository.UpdateProfile(request);
                    return Ok(errors);
                }

            }
            catch (Exception ex)
            {
                List<string> errors = new List<string>();
                errors.Add(ex.Message);
                return BadRequest(errors);
            }
        }

        [HttpPut("ChangePassword")]
        public IActionResult ChangePassword([FromBody] ChangePasswordRequest request)
        {

            try
            {
                List<string> errors = new AccountValidate(accountRepository).ValidateChangePasswordRequest(request);
                if (errors.Count > 0)
                {
                    return BadRequest(errors);
                }
                else
                {
                    accountRepository.ChangePassword(request);
                    return Ok(errors);
                }

            }
            catch (Exception ex)
            {
                List<string> errors = new List<string>();
                errors.Add(ex.Message);
                return BadRequest(errors);
            }
        }
    }
}
