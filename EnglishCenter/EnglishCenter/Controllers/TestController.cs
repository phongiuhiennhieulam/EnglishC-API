using EnglishCenter.Repository;
using EnglishCenter.Request;
using EnglishCenter.Validate;
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
        [HttpGet("{id}")]
        public IActionResult GetDetailTest(int id)
        {
            try
            {
                return Ok(testRepository.getDetailTest(id));
            }
            catch (Exception ex)
            {

                return BadRequest(null);
            }
        }
        [HttpDelete("{id}/{status}")]
        public void DeleteTest(int id, bool? status)
        {
            try
            {
                testRepository.actionTest(id,status);
            }
            catch (Exception ex)
            {
            }
        }
        [HttpPost("AddTest")]
        public IActionResult AddTest(AddTestRequest request)
        {
            try
            {
                List<string> errors = new TestValidate().validateAddTest(request);
                if (errors.Count == 0)
                {
                    testRepository.addTest(request);
                    return Ok(errors);
                }
                else
                {
                    return BadRequest(errors);
                }
                
            }
            catch (Exception ex)
            {
                List<string> errors = new List<string>();
                errors.Add(ex.Message);
                return BadRequest(errors);
            }
        }
        [HttpGet("UpdateTest")]
        public IActionResult UpdateTest(int id)
        {
            try
            {
                return Ok(testRepository.GetUpdateTest(id));

            }
            catch (Exception ex)
            {
                List<string> errors = new List<string>();
                errors.Add(ex.Message);
                return BadRequest(errors);
            }
        }
        [HttpPost("UpdateTest")]
        public IActionResult UpdateTest(UpdateTestRequest request)
        {
            try
            {
                List<string> errors = new TestValidate().validateUpdateTest(request);
                if (errors.Count == 0)
                {
                    testRepository.updateTest(request);
                    return Ok(errors);
                }
                else
                {
                    return BadRequest(errors);
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
