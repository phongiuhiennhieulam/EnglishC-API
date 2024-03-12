namespace EnglishCenter.Request
{
    public class UpdateTestRequest
    {
        public int id { get; set; }
        public string name { get; set; }

        public int time { get; set; }

        public List<int> listQuestion { get; set; }
    }
}
