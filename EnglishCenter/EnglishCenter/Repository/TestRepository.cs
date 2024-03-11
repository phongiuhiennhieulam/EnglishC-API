using EnglishCenter.DTO;
using EnglishCenter.Model;
using EnglishCenter.Request;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace EnglishCenter.Repository
{
    public class TestRepository : ITestRepository
    {
        private readonly assginPRN231Context _context;
        public TestRepository(assginPRN231Context context)
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
                var questions = _context.Tests.Include(x => x.Questions).Where(x => x.Id == test.Id).SingleOrDefault().Questions;
                List<ShowQuestionDTO> listQuestion = new List<ShowQuestionDTO>();
                foreach(var question in questions)
                {
                    ShowQuestionDTO showDTO = new ShowQuestionDTO()
                    {
                        QuestionContent = question.QuestionContent,
                        id = (int)question.Id,


                    };
                    var answers = _context.Answers.Where(x => x.QuestId == question.Id).ToList();
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

        public void actionTest(int id,bool? status)
        {
            try
            {

                var test = _context.Tests.SingleOrDefault(x => x.Id == id);
                test.Status = status;
                _context.Tests.Update(test);
                _context.SaveChanges();


            }
            catch (Exception ex)
            {
                
            }
        }
        public void addTest(AddTestRequest request)
        {
            try
            {

                Test t = new Test();
                t.Title = request.name;
                t.CreateDate = DateTime.Now;
                t.Status = true;
                t.Time = request.time;
                _context.Tests.Add(t);
                _context.SaveChanges();
                ICollection<Question> listQ = t.Questions;
                foreach (var id in request.listQuestion)
                {
                    
                    var question = _context.Questions.SingleOrDefault(x => x.Id == id);
                    ICollection<Test> listT = question.Tests;
                    listT.Add(t);
                    question.Tests = listT;
                    
                    listQ.Add(question);
                    
                }
                t.Questions = listQ;
                _context.SaveChanges();



            }
            catch (Exception ex)
            {

            }
        }
    }
}
