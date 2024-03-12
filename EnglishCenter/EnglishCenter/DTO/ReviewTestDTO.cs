namespace EnglishCenter.DTO
{
    public class ReviewTestDTO
    {
        public int? testID { get; set; }

        public string testName { get; set; }

        public int? time { get; set; }

        public DateTime? doingTestDate { get; set; }

        public float totalMark { get; set; }

        public List<ReviewQuestionDTO> reviews { get; set; }


    }
}
