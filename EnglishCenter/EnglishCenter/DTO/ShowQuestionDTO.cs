namespace EnglishCenter.DTO
{
    public class ShowQuestionDTO
    {
        public string QuestionContent { get; set; }

        public List<AnswerDTO> Answers { get; set; }
    }
}
