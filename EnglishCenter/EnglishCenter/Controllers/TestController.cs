using EnglishCenter.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EnglishCenter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly ITestRepository testRepository;
        public TestController(ITestRepository _testRepository)
        {
            testRepository = _testRepository;
        }
        [HttpGet("ListTest")]
        public IActionResult GetListTest()
        {
            try
            {
                return Ok(testRepository.getAllTest());
            }
            catch (Exception ex)
            {

                return BadRequest(null);
            }
        }
    }
}
