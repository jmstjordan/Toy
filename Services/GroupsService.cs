using System;
using System.Collections.Generic;
using toy.Controllers;
using toy.Models;

namespace toy.Services
{
    public class GroupsService : IGroupsService
    {
        private string groupsPath;

        public GroupsService(string groupsPath)
        {
            this.groupsPath = groupsPath;
        }

        public Group GetGroup(string gid)
        {
            throw new NotImplementedException();
        }

        public List<Group> GetGroups()
        {
            throw new NotImplementedException();
        }

        public List<Group> GetGroupsByQuery(string name, string gid, List<string> members)
        {
            throw new NotImplementedException();
        }
    }
}
