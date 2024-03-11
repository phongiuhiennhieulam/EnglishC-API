using EnglishCenter.DTO;
using EnglishCenter.Model;
using EnglishCenter.Request;
using Microsoft.EntityFrameworkCore;

namespace EnglishCenter.Repository
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly AssginPRN231Context _context;
        public QuestionRepository(AssginPRN231Context context)
        {
            _context = context;

        }
        public List<ShowQuestionDTO> getAllQuestion()
        {
            var listQuestion = _context.Questions.Include(x => x.Answers).ToList();
            List<ShowQuestionDTO> listReturn = new List<ShowQuestionDTO>();
            try
            {
                
                foreach (var item in listQuestion)
                {
                    ShowQuestionDTO questDTO = new ShowQuestionDTO();
                    questDTO.id = item.Id;
                    questDTO.QuestionContent = item.QuestionContent;
                    List<AnswerDTO> listAnswerDTO = new List<AnswerDTO>();
                    var listAnswer = _context.Answers.Where(x => x.QuestId == item.Id).ToList();
                    
                    foreach (var answer in listAnswer)
                    {
                        AnswerDTO dto = new AnswerDTO()
                        {
                            id = answer.Id,
                            content = answer.AnsContent,
                            isTrue = answer.IsTrue
                        };
                        listAnswerDTO.Add(dto);


                    }
                    questDTO.Answers = listAnswerDTO;
                    listReturn.Add(questDTO);
                }

            }
            catch (Exception ex)
            {

            }
            return listReturn;
        }
        public void deleteQuestion(int id)
        {
           
            try
            {
                var question = _context.Questions.Where(x => x.Id == id).FirstOrDefault();
                var answer = _context.Answers.Where(x => x.QuestId == id).ToList();
                _context.Answers.RemoveRange(answer);
                _context.Questions.Remove(question);
                _context.SaveChanges();

            }
            catch (Exception ex)
            {

            }
        }
        public void addQuestion(AddQuestionRequest request)
        {

            try
            {
                Question question = new Question();
                question.Status = true;
                question.QuestionContent = request.questionContent;
                _context.Questions.Add(question);
                _context.SaveChanges();
                foreach(var item in request.list)
                {
                    Answer answer = new Answer()
                    {
                        AnsContent = item.answerContent,
                        IsTrue = item.isTrue,
                        QuestId = question.Id
                    };
                    _context.Answers.Add(answer);
                    _context.SaveChanges();
                }

            }
            catch (Exception ex)
            {

            }
        }
    }
}
