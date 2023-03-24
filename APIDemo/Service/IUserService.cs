using APIDemo.Models;
using Microsoft.AspNetCore.Identity;
using static APIDemo.Service.IUserService;

namespace APIDemo.Service
{
    public interface IUserService
    {
        public User Get(UserLogin userLogin);
    }
}
