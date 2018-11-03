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
        public IActionResult GetUsers()
        {
            return Ok(usersService.GetUsers());
        }

        [HttpGet("query")]
        public IActionResult GetUsersByQuery(string name, string uid, string gid, string comment, string home, string shell)
        {
            return Ok(usersService.GetUsersByQuery(name, uid, gid, comment, home, shell));
        }

        [HttpGet("{uid}")]
        public IActionResult GetUserByUid(string uid)
        {
            var user = usersService.GetUser(uid);
            if(user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpGet("{uid}/groups")]
        public IActionResult GetUserGroupsByUid(string uid)
        {
            return Ok(usersService.GetUserGroups(uid));
        }
    }
}
