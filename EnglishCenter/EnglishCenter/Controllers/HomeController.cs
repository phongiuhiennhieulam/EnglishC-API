using EnglishCenter.Repository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EnglishCenter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : Controller
    {
        private readonly IAccountRepository accountRepository;
        public HomeController(IAccountRepository _accountRepository)
        {
            accountRepository = _accountRepository;
        }
        
    }
}
