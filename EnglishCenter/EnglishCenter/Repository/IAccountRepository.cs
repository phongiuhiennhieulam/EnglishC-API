using EnglishCenter.DTO;
using EnglishCenter.Model;
using EnglishCenter.Request;

namespace EnglishCenter.Repository
{
    public interface IAccountRepository
    {
        List<ShowAccountDTO> getAllAccount();
        void ActionAccount(int account_id,int status);

        void AddAccount(AddAccountRequest request);

        void UpdateProfile(UpdateProfileRequest request);

        void ChangePassword(ChangePasswordRequest request);

        User GetAccountByUsernamePassword(string email, string password);

        User GetAccountByEmail(string email);
    }
}
