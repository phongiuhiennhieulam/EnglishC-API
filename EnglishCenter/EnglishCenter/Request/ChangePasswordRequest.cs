namespace EnglishCenter.Request
{
    public class ChangePasswordRequest
    {
        public string email { get; set; }

        public string oldPassword { get; set; }

        public string newPassword { get; set; }

        public string resetPassword { get; set; }
    }
}
