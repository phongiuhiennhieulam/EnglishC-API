namespace EnglishCenter.DTO
{
    public class ShowTestUpdateDTO
    {
        public int id { get; set; }

        public string name { get; set; }

        public int? time { get; set; }

        public bool? status { get; set; }

        public List<ShowQuestionDTO> questionInTest { get; set; }

        public List<ShowQuestionDTO> questionOutTest { get; set; }


    }
}
