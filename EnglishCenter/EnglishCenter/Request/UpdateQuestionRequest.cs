namespace EnglishCenter.Request
{
    public class UpdateQuestionRequest
    {
        public int questionID { get; set; }
        public string questionContent { get; set; }

        public List<UpdateAnswerQuestionRequest> answerQuestions { get; set; }
    }
}
