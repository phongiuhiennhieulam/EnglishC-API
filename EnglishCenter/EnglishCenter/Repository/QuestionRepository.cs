using EnglishCenter.DTO;
using EnglishCenter.Model;
using EnglishCenter.Request;
using Microsoft.EntityFrameworkCore;

namespace EnglishCenter.Repository
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly assginPRN231Context _context;
        public QuestionRepository(assginPRN231Context context)
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
        public void updateQuestion(UpdateQuestionRequest request)
        {

            try
            {
                var question = _context.Questions.SingleOrDefault(x => x.Id == request.questionID);
                var answers = _context.Answers.Where(x => x.QuestId.Equals(request.questionID)).ToList();
                _context.Answers.RemoveRange(answers);
                _context.SaveChanges();
                question.QuestionContent = request.questionContent;
                foreach (var item in request.answerQuestions)
                {
                    
                        Answer answer = new Answer()
                        {
                            QuestId = question.Id,
                            AnsContent = item.answerContent,
                            IsTrue = item.isTrue,
                        };
                        _context.Answers.Add(answer);
                }
                _context.SaveChanges();

            }
            catch (Exception ex)
            {

            }
        }
        public ShowQuestionDTO showQuestion(int id)
        {

            try
            {
                var question = _context.Questions.SingleOrDefault(x => x.Id == id);
                var answers = _context.Answers.Where(x => x.QuestId == id).ToList();
                List<AnswerDTO> list = new List<AnswerDTO>();
                foreach (var item in answers)
                {

                    AnswerDTO answer = new AnswerDTO()
                    {
                        content = item.AnsContent,
                        isTrue = item.IsTrue,
                        id = item.Id
                    };
                    list.Add(answer);
                }
                return new ShowQuestionDTO()
                {
                    Answers = list,
                    QuestionContent = question.QuestionContent,
                    id = id
                };

            }
            catch (Exception ex)
            {
                return null;
            }
            return null;
        }
    }
}
