using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            string line;
            StreamReader file = new StreamReader(groupsPath);
            while ((line = file.ReadLine()) != null)
            {
                Group group = BuildGroup(line);
                if (group != null && group.Gid == gid)
                {
                    return group;
                }
            }
            file.Close();
            return null;
        }

        public List<Group> GetGroups()
        {
            List<Group> groups = new List<Group>();
            string line;
            StreamReader file = new StreamReader(groupsPath);
            while ((line = file.ReadLine()) != null)
            {
                Group group = BuildGroup(line);
                if (group != null)
                {
                    groups.Add(group);
                }
            }
            file.Close();
            return groups;
        }

        public List<Group> GetGroupsByQuery(string name, string gid, List<string> members)
        {
            List<Group> groups = new List<Group>();
            string line;
            StreamReader file = new StreamReader(groupsPath);
            while ((line = file.ReadLine()) != null)
            {
                Group group = BuildGroup(line);
                if (group != null)
                {
                    if (!string.IsNullOrEmpty(name) && group.Name != name)
                    {
                        continue;
                    }
                    if (!string.IsNullOrEmpty(gid) && group.Gid != gid)
                    {
                        continue;
                    }
                    if (members.Count() != 0 && members.Intersect(group.Members).Count() != members.Count())
                    {
                        continue;
                    }
                    groups.Add(group);
                }
            }
            file.Close();
            return groups;
        }

        private Group BuildGroup(string line)
        {
            if (line.StartsWith("#", StringComparison.Ordinal))
            {
                return null;
            }
            string[] parts = line.Split(":");
            Console.WriteLine(parts);
            return new Group
            {
                Name = parts[0],
                Gid = parts[2],
                Members = BuildMembers(parts[3])
            };
        }

        private List<string> BuildMembers(string lineMembers)
        {
            if(string.IsNullOrEmpty(lineMembers))
            {
                return new List<string>();
            }
            return new List<string>(lineMembers.Split(","));
        }
    }
}
