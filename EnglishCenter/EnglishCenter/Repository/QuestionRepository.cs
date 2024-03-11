using EnglishCenter.DTO;
using EnglishCenter.Model;
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
    }
}
