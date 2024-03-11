namespace EnglishCenter.DTO
{
    public class ShowQuestionDTO
    {
        public int id { get; set; }
        public string QuestionContent { get; set; }

        public List<AnswerDTO> Answers { get; set; }
    }
}
