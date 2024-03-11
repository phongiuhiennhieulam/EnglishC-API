using EnglishCenter.DTO;
using EnglishCenter.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace EnglishCenter.Repository
{
    public class TestRepository : ITestRepository
    {
        private readonly AssginPRN231Context _context;
        public TestRepository(AssginPRN231Context context)
        {
            _context = context;

        }
        public List<ShowTestDTO> getAllTest()
        {
            var list = _context.Tests.ToList();
            List<ShowTestDTO> returnList = new();
            try
            {

                foreach (var item in list)
                {
                    ShowTestDTO show = new ShowTestDTO()
                    {
                       id = item.Id,
                       name = item.Title,
                       time = item.Time,
                       date = item.CreateDate,
                       status = item.Status
                    };
                    returnList.Add(show);
                }

            }
            catch (Exception ex)
            {

            }
            return returnList;
        }

        public ShowTestDTO getDetailTest(int id)
        {
            try
            {

                var test = _context.Tests.SingleOrDefault(x => x.Id == id);
                ShowTestDTO dto = new ShowTestDTO()
                {
                    id = test.Id,
                    name = test.Title,
                    time = test.Time,
                    date = test.CreateDate,
                    status = test.Status
                };
                var questions = _context.TestQuestions.Include(x => x.Question).Where(x => x.TestId == test.Id).ToList();
                List<ShowQuestionDTO> listQuestion = new List<ShowQuestionDTO>();
                foreach(var question in questions)
                {
                    ShowQuestionDTO showDTO = new ShowQuestionDTO()
                    {
                        QuestionContent = question.Question.QuestionContent,
                        id = (int)question.QuestionId,


                    };
                    var answers = _context.Answers.Where(x => x.QuestId == question.QuestionId).ToList();
                    List<AnswerDTO> list = new List<AnswerDTO>();
                    foreach(var answer in answers)
                    {
                        AnswerDTO an = new AnswerDTO()
                        {
                            content = answer.AnsContent,
                            id = answer.Id,
                            isTrue = answer.IsTrue
                        };
                        list.Add(an);
                    }
                    showDTO.Answers = list;
                    listQuestion.Add(showDTO);
                }
                dto.questions = listQuestion;
                return dto;


            }
            catch (Exception ex)
            {
                return null;
            }
            return null;
        }
    }
}
