namespace EnglishCenter.DTO
{
    public class ShowTestDTO
    {
        public int id { get; set; }

        public string name { get; set; }

        public DateTime? date { get; set; }

        public int? time { get; set; }   

        public bool? status { get; set; }

        public List<ShowQuestionDTO> questions { get; set; }
    }
}
