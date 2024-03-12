using EnglishCenter.Repository;
using EnglishCenter.Request;
using EnglishCenter.Validate;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = "Admin,Teacher,Student")]
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
        [Authorize(Roles = "Admin,Teacher,Student")]
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
        [HttpGet("ListTestDone")]
        [Authorize(Roles = "Student,Teacher")]
        public IActionResult ListTestDone(int userID)
        {
            try
            {
                return Ok(testRepository.GetListDoneTest(userID));

            }
            catch (Exception ex)
            {
                List<string> errors = new List<string>();
                errors.Add(ex.Message);
                return BadRequest(errors);
            }
        }
        [HttpPost("UpdateTest")]
        [Authorize(Roles = "Admin")]
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
        
        [HttpPost("MarkTest")]
        //[Authorize(Roles = "Teacher,Student")]
        public IActionResult MarkTest(MarkTestRequest request)
        {
            try
            {

                    
                    return Ok(testRepository.markTest(request));
               

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
