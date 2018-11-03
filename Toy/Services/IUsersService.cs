using System.Collections.Generic;
using toy.Models;

namespace toy.Controllers
{
    public interface IUsersService
    {
        List<User> GetUsers();
        User GetUser(string uid);
        List<Group> GetUserGroups(string uid);
        List<User> GetUsersByQuery(string name, string uid, string gid, string comment, string home, string shell);
    }
}