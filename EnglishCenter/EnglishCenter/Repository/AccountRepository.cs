using EnglishCenter.DTO;
using EnglishCenter.Model;
using EnglishCenter.Request;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Collections.Generic;

namespace EnglishCenter.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly AssginPRN231Context _context;
        public AccountRepository(AssginPRN231Context context)
        {
            _context = context;

        }

        public void ActionAccount(int account_id, int status)
        {
            
            try
            {
                var account = _context.Users.SingleOrDefault(x => x.Id == account_id);
                if (account == null)
                {
                    return;
                }
                else {
                    account.Status = (status == 1) ? true:false ;
                }
                _context.Users.Update(account);
                _context.SaveChanges();
                

               

            }
            catch (Exception ex)
            {

            }
            
        }

        public void UpdateProfile(UpdateProfileRequest request)
        {
            try
            {

                var user = _context.Users.SingleOrDefault(x => x.Email.Equals(request.email));
                user.Address = request.address;
                user.Phone = request.phone;
                user.Name = request.name;
                _context.Users.Update(user);
                _context.SaveChanges();

            }
            catch (Exception ex)
            {

            }
        }

        public void ChangePassword(ChangePasswordRequest request)
        {
            try
            {

               var user = _context.Users.SingleOrDefault(x => x.Email.Equals(request.email));
                user.Password = request.newPassword; 
                _context.SaveChanges();

            }
            catch (Exception ex)
            {

            }
        }

        public User GetAccountByEmail(string email)
        {
            try
            {
                return _context.Users.Include(x => x.Role).SingleOrDefault(x => x.Email.Equals(email));

            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public User GetAccountByUsernamePassword(string email, string password)
        {
            try
            {
                return _context.Users.SingleOrDefault(x => x.Email.Equals(email) && x.Password.Equals(password));

            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public void AddAccount(AddAccountRequest request)
        {
            try
            {

                User account = new User()
                {
                    Name = request.name,
                    Email = request.email,
                    Password = request.password,
                    Status = true,
                    RoleId = request.roleId,
                    Address = request.address,
                    Phone = request.phone
                };
                _context.Users.Add(account);
                _context.SaveChanges();

            }
            catch (Exception ex)
            {

            }
        }

        public List<ShowAccountDTO> getAllAccount()
        {
            var list = _context.Users.Include(x => x.Role).ToList();
            List<ShowAccountDTO> returnList = new ();
            try
            {
               
                foreach(var item in list)
                {
                    ShowAccountDTO show = new ShowAccountDTO()
                    {
                        id = item.Id,
                        name = item.Name,
                        email = item.Email,
                        role = item.Role.Name,
                        status = (item.Status == true)? "Active":"Block"
                    };
                    returnList.Add(show);
                }

            }catch(Exception ex)
            {

            }
            return returnList;
        }
        
    }
}
