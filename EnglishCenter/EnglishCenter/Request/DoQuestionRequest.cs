using EnglishCenter.DTO;

namespace EnglishCenter.Request
{
    public class DoQuestionRequest
    {
        public int questionID { get; set; }

        public List<AnswerDTO>? answerID { get; set; }

    }
}
