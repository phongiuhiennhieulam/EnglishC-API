namespace EnglishCenter.Request
{
    public class AddQuestionRequest
    {
        public string questionContent { get; set; }

        public List<AddAnswerQuestionRequest> list { get; set; }
    }
}
