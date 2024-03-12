using EnglishCenter.DTO;
using EnglishCenter.Model;
using EnglishCenter.Request;
using Microsoft.EntityFrameworkCore;
using System.Collections;
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
                        Answers = GetAnswerOfQuestion(question.Id)

                    };
                    
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

        public List<AnswerDTO> GetCorrectAnswerOfQuestion(int questID)
        {
            List<AnswerDTO> listAnswerDTO = new List<AnswerDTO>();
            var listAnswer = _context.Answers.Where(x => x.QuestId == questID).ToList();

            foreach (var answer in listAnswer)
            {
                if (answer.IsTrue == true)
                {
                    AnswerDTO dtoAnswer = new AnswerDTO()
                    {
                        id = answer.Id,
                        content = answer.AnsContent,
                        isTrue = answer.IsTrue
                    };
                    listAnswerDTO.Add(dtoAnswer);
                }
                


            }
            return listAnswerDTO;
        }
        public List<AnswerDTO> GetAnswerOfQuestion(int questID)
        {
            List<AnswerDTO> listAnswerDTO = new List<AnswerDTO>();
            var listAnswer = _context.Answers.Where(x => x.QuestId == questID).ToList();

            foreach (var answer in listAnswer)
            {
                AnswerDTO dtoAnswer = new AnswerDTO()
                {
                    id = answer.Id,
                    content = answer.AnsContent,
                    isTrue = answer.IsTrue
                };
                listAnswerDTO.Add(dtoAnswer);


            }
            return listAnswerDTO;
        }
        public ShowTestUpdateDTO GetUpdateTest(int id)
        {
            try
            {
                var question = _context.Questions.ToList();
                var questionTest = _context.Tests.Include(x => x.Questions).SingleOrDefault(x => x.Id == id).Questions;

                List<ShowQuestionDTO> questionInTest = new List<ShowQuestionDTO>();
                foreach (var item in questionTest)
                {
                    questionInTest.Add(new ShowQuestionDTO()
                    {
                        id = item.Id,
                        QuestionContent = item.QuestionContent,
                        Answers = GetAnswerOfQuestion(item.Id)
                    });
                }
                List<ShowQuestionDTO> questionOutTest = new List<ShowQuestionDTO>();
                var test = _context.Tests.SingleOrDefault(x => x.Id == id);
                foreach(var q in question)
                {
                    bool check = true;
                    foreach(var i in questionTest)
                    {
                        if(q.Id == i.Id)
                        {
                            check = false;
                        }
                    }
                    if (check)
                    {
                        var item = _context.Questions.SingleOrDefault(x => x.Id == q.Id);
                        ShowQuestionDTO dto = new ShowQuestionDTO()
                        {
                            id = item.Id,
                            QuestionContent = item.QuestionContent,
                            Answers = GetAnswerOfQuestion(item.Id)
                        };
                        
                        questionOutTest.Add(dto);
                    }
                }
                return new ShowTestUpdateDTO()
                {
                    id = id,
                    name = test.Title,
                    time = test.Time,
                    status = test.Status,
                    questionInTest = questionInTest,
                    questionOutTest = questionOutTest

                };




            }
            catch (Exception ex)
            {
                return null;
            }
            return null;
        }
        public void updateTest(UpdateTestRequest request)
        {
            try
            {
                List<Question> list = new List<Question>();

                var t = _context.Tests.Include(x => x.Questions).SingleOrDefault(x => x.Id == request.id);
                t.Title = request.name;
                t.Time = request.time;
                t.Questions.Clear();
                _context.Tests.Update(t);
                _context.SaveChanges();
                foreach (var id in request.listQuestion)
                {

                    var question = _context.Questions.SingleOrDefault(x => x.Id == id);
                    list.Add(question);

                }
                t.Questions = list;
                _context.Tests.Update(t);
                _context.SaveChanges();



            }
            catch (Exception ex)
            {

            }
        }
        public ReviewTestDTO markTest(MarkTestRequest request)
        {
            try
            {
                Mark m = new Mark();
                m.TestId = request.testID;
                m.UserId = request.userID;
                m.CreateDate = DateTime.Now;
                _context.Marks.Add(m);
                _context.SaveChanges();


                ReviewTestDTO dto = new ReviewTestDTO();
                List<ReviewQuestionDTO> listQuestion = new List<ReviewQuestionDTO>();

                float totalMark = 0;
                int totalQuestion = request.doQuestion.Count;
                int totalTrue = 0;
                foreach (var item in request.doQuestion)
                {
                    var question = _context.Questions.SingleOrDefault(x => x.Id == item.questionID);
        
                    List<AnswerDTO> listAnswer = GetCorrectAnswerOfQuestion(question.Id);
                    ReviewQuestionDTO review = new ReviewQuestionDTO();

                    review.listAnswer = listAnswer;
                    review.listChoosen = item.answerID;
                    review.questionContent = question.QuestionContent;
                    bool check = true;
                    int count = 0;
                    if (item.answerID == null)
                    {
                        review.isTrue = false;
                        review.mark = 0;
                        check = false;
                    }
                    else
                    {
                        foreach (var answer in item.answerID)
                        {

                            if (answer.isTrue == false)
                            {
                                check = false;
                            }
                            else
                            {
                                count++;
                            }

                        }
                    }
                    
                    if(check == true && count == listAnswer.Count)
                    {
                        totalMark += ((float)10 / totalQuestion);
                        review.isTrue = true;
                        review.mark =((float)10 / totalQuestion);
                    }
                    else
                    {
                        review.isTrue = false;
                        review.mark = 0;
                    }
                    listQuestion.Add(review);
                }
                Mark newMark = _context.Marks.Include(X => X.Test).SingleOrDefault(x => x.Id == m.Id);
                newMark.Mark1 = totalMark;
                _context.Marks.Update(newMark);
                _context.SaveChanges();
                foreach(var rv in request.doQuestion)
                {
                    if(rv.answerID == null)
                    {
                        _context.Reviews.Add(new Review()
                        {
                            QuestionId = rv.questionID,
                            MarkId = newMark.Id
                        });
                        _context.SaveChanges();
                    }
                    else
                    {
                        foreach (var an in rv.answerID)
                        {
                            _context.Reviews.Add(new Review()
                            {
                                QuestionId = rv.questionID,
                                AnswerId = an.id,
                                MarkId = newMark.Id
                            });
                            _context.SaveChanges();
                        }
                    }
                  
                }
                dto.testID = newMark.TestId;
                dto.testName = newMark.Test.Title;
                dto.totalMark = totalMark;
                dto.time = newMark.Test.Time;
                dto.doingTestDate = newMark.CreateDate;
                dto.reviews = listQuestion;
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
