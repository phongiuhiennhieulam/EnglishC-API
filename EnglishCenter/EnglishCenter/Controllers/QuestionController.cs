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
    public class QuestionController : ControllerBase
    {
        private readonly IQuestionRepository questionRepository;
        public QuestionController(IQuestionRepository _questionRepository)
        {
            questionRepository = _questionRepository;
        }

        [HttpGet("ShowListQuestion")]
        [Authorize(Roles = "Admin")]
        public IActionResult ShowListQuestion()
        {
            try
            {

                return Ok(questionRepository.getAllQuestion());
            }
            catch (Exception ex)
            {

                return BadRequest(null);
            }
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public void DeleteQuestion(int id)
        {
            try
            {
                questionRepository.deleteQuestion(id);
            }
            catch (Exception ex)
            {

            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult AddQuestion([FromBody] AddQuestionRequest request)
        {
            try
            {
                List<string> error = new QuestionValidate().validateAddQuestion(request);
                if (error.Count == 0) {
                    questionRepository.addQuestion(request);
                    return Ok();
                }
                else
                {
                    return BadRequest(error);
                }
                
            }
            catch (Exception ex)
            {
                List<string> errors = new List<string>();
                errors.Add(ex.Message);
                return BadRequest(errors);
            }
        }
        [HttpPut]
        [Authorize(Roles = "Admin")]
        public IActionResult UpdateQuestion([FromBody] UpdateQuestionRequest request)
        {
            try
            {
                List<string> error = new QuestionValidate().validateUpdateQuestion(request);
                if (error.Count == 0)
                {
                    questionRepository.updateQuestion(request);
                    return Ok();
                }
                else
                {
                    return BadRequest(error);
                }

            }
            catch (Exception ex)
            {
                List<string> errors = new List<string>();
                errors.Add(ex.Message);
                return BadRequest(errors);
            }
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult ShowQuestion(int id)
        {
            try
            {

                    
                    return Ok(questionRepository.showQuestion(id));


            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
