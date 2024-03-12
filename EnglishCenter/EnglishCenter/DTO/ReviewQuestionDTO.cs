namespace EnglishCenter.DTO
{
    public class ReviewQuestionDTO
    {
        public string questionContent { get; set; }

        public List<AnswerDTO>? listChoosen { get; set; }

        public List<AnswerDTO>? listAnswer { get; set; }  

        public bool isTrue { get; set; }

        public float mark { get; set; }
    }
}
