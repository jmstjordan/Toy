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
        public async Task<List<Group>> GetGroups()
        {
            return groupsService.GetGroups();
        }

        [HttpGet("query")]
        public async Task<List<Group>> GetGroupsByQuery(string name, string gid, string[] members)
        {
            return groupsService.GetGroupsByQuery(name, gid, null);
        }

        [HttpGet("{gid}")]
        public async Task<Group> GetByGid(string gid)
        {
            return groupsService.GetGroup(gid);
        }

    }
}
