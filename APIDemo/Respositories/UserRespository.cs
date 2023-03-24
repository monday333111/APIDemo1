//using System.ComponentModel.DataAnnotations;

using APIDemo.Models;
using Microsoft.AspNetCore.Identity;
using System.Data;

namespace APIDemo.Respositories
{
    public class UserRespository
    {
        public static List<User> Users = new()
        {
            new() { UserName = "ally_admin", EmailAddress = "allyadmin@rmail.com", Password = "passw0rd",GivenName="ally",SurName="Snow",Role="Administrator"},
            new() { UserName = "bob_standard", EmailAddress = "bobstandard@rmail.com", Password = "passw0rd123",GivenName="bob",SurName="Burton",Role="Standard"}
        };
    }
}
