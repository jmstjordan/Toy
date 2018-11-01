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
    public class GroupsController : Controller
    {
        private IGroupsService groupsService;
        public GroupsController(IGroupsService groupsService)
        {
            this.groupsService = groupsService;
        }

        [HttpGet]
        public IActionResult GetGroups()
        {
            return Ok(groupsService.GetGroups());
        }

        [HttpGet("query")]
        public IActionResult GetGroupsByQuery(string name, string gid, List<string> members)
        {
            return Ok(groupsService.GetGroupsByQuery(name, gid, members));
        }

        [HttpGet("{gid}")]
        public IActionResult GetByGid(string gid)
        {
            var group = groupsService.GetGroup(gid);
            if(group == null)
            {
                return NotFound();
            }
            return Ok(group);
        }

    }
}
