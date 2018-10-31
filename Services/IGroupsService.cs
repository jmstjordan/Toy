using System.Collections.Generic;
using toy.Models;

namespace toy.Controllers
{
    public interface IGroupsService
    {
        List<Group> GetGroups();
        List<Group> GetGroupsByQuery(string name, string gid, List<string> members);
        Group GetGroup(string gid);
    }
}