using EnglishCenter.Repository;
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
    }
}
