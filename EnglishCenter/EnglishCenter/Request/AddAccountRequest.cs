namespace EnglishCenter.Request
{
    public class AddAccountRequest
    {
        public string phone { get; set; }

        public string address { get; set; }

        public int roleId { get; set; }

        public string password { get; set; }

        public string email { get; set; }

        public string name { get; set; }
    }
}
