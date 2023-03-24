using APIDemo.Models;
using APIDemo.Respositories;

namespace APIDemo.Service
{
    public class UserService :IUserService
    {
        public User Get(UserLogin userLogin)
        {
            User user = UserRespository.Users.FirstOrDefault(o => o.UserName.Equals(userLogin.Username, StringComparison.OrdinalIgnoreCase) && o.Password.Equals(userLogin.Password));
            return user;
        }
    }
}
