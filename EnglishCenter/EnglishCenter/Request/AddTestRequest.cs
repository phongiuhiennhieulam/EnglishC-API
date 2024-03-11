namespace EnglishCenter.Request
{
    public class AddTestRequest
    {
        public string name { get; set; }

        public int time { get; set; }

        public List<int> listQuestion { get; set; }
    }
}
