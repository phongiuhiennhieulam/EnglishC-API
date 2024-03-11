using EnglishCenter.Repository;
using EnglishCenter.Request;
using System.Text.RegularExpressions;

namespace EnglishCenter.Validate
{
    public class AccountValidate
    {
        private readonly IAccountRepository _accountRepository;
        public AccountValidate(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }



        public Boolean ValidateExistEmail(String email)
        {
            var accounts = _accountRepository.getAllAccount();
            foreach(var account in accounts)
            {
                if (email.ToUpper().Equals(account.email.ToUpper()))
                {
                    return false;
                }
            }
            return true;
        }
        public Boolean ValidatePhone(String phone)
        {
            Regex regex = new Regex(@"^\d{10}$");
            return regex.IsMatch(phone);
        }
        public Boolean ValidateLength(String input)
        {
            if (input.Length >= 50 || string.IsNullOrEmpty(input))
            {
                return false;
            }
            else {
                return true;
            }
        }
        public List<string> ValidateAddAccountRequest(AddAccountRequest request)
        {
            List<string> errors = new List<string>();
            if(!ValidatePhone(request.phone))
            {
                errors.Add("Phone contains exact of 10 digit characters");
            }
            if (!ValidateExistEmail(request.email))
            {
                errors.Add("Email is exist");
            }
            if(!ValidateLength(request.address) || !ValidateLength(request.email) || !ValidateLength(request.name) || !ValidateLength(request.password))
            {
                errors.Add("Address,Email,Name,Password must not empty and not over 50 characters");
            }
            return errors;
        }
        public List<string> ValidateUpdateProfileRequest(UpdateProfileRequest request)
        {
            List<string> errors = new List<string>();
            if (!ValidatePhone(request.phone))
            {
                errors.Add("Phone contains exact of 10 digit characters");
            }
            if (!ValidateLength(request.address) || !ValidateLength(request.name))
            {
                errors.Add("Address,Name must not empty and not over 50 characters");
            }
            return errors;
        }

        public Boolean ValidateCorrectOldPassword(String email,String oldPassword)
        {
            var user = _accountRepository.GetAccountByUsernamePassword(email, oldPassword);
            if(user != null)
            {
                return true;
            }
            return false;


        }
        public Boolean ValidateNewAndResetPassword(String newPassword, String resetPassword)
        {
            if (newPassword.Equals(resetPassword))
            {
                return true;
            }
            return false;


        }
        public List<string> ValidateChangePasswordRequest(ChangePasswordRequest request)
        {
            List<string> errors = new List<string>();
            if (!ValidateLength(request.oldPassword) || !ValidateLength(request.newPassword) || !ValidateLength(request.resetPassword))
            {
                errors.Add("All password must not empty and over 50 characters");
            }
            if (!ValidateCorrectOldPassword(request.email,request.oldPassword))
            {
                errors.Add("Wrong old password");
            }
            if (!ValidateNewAndResetPassword(request.newPassword,request.resetPassword))
            {
                errors.Add("New and reset password must equals");
            }
            return errors;
        }
    }
}
