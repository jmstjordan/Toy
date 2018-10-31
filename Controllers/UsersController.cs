using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using toy.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace toy.Controllers
{
    [Route("[controller]")]
    public class UsersController : Controller
    {
        private IUsersService usersService;

        public UsersController(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        [HttpGet]
        public async Task<List<User>> GetUsers()
        {
            return usersService.GetUsers();
        }

        [HttpGet("query")]
        public async Task<List<User>> GetUsersByQuery(string name, string uid, string gid, string comment, string home, string shell)
        {
            return usersService.GetUserByQuery(name, uid, gid, comment, home, shell);
        }

        [HttpGet("{uid}")]
        public async Task<User> GetUserByUid(string uid)
        {
            return usersService.GetUser(uid);
        }

        [HttpGet("{uid}/groups")]
        public async Task<List<Group>> GetUserGroupsByUid(string uid)
        {
            return usersService.GetUserGroups(uid);
        }
    }
}
