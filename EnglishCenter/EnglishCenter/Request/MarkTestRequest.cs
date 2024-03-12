namespace EnglishCenter.Request
{
    public class MarkTestRequest
    {
        public int testID { get; set; }

        public int userID { get; set; }


        public List<DoQuestionRequest> doQuestion { get; set; }
    }
}
