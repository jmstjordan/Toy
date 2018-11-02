using System;
using System.Collections.Generic;
using System.IO;
using toy.Controllers;
using toy.Models;

namespace toy.Services
{
    public class UsersService : IUsersService
    {
        private string userPath;
        private IGroupsService groupsService;

        public UsersService(string userPath, IGroupsService groupsService)
        {
            this.userPath = userPath;
            this.groupsService = groupsService;
        }

        public User GetUser(string uid)
        {
            string line;
            StreamReader file = new StreamReader(userPath);
            while ((line = file.ReadLine()) != null)
            {
                User user = BuildUser(line);
                if(user != null && user.Uid == uid)
                {
                    return user;
                }
            }
            file.Close();
            return null;
        }

        public List<User> GetUserByQuery(string name, string uid, string gid, string comment, string home, string shell)
        {
            List<User> users = new List<User>();
            string line;
            StreamReader file = new StreamReader(userPath);
            while ((line = file.ReadLine()) != null)
            {
                User user = BuildUser(line);
                if (user != null)
                {
                    if (!string.IsNullOrEmpty(name) && user.Name != name)
                    {
                        continue;
                    }
                    if (!string.IsNullOrEmpty(uid) && user.Uid != uid)
                    {
                        continue;
                    }
                    if (!string.IsNullOrEmpty(gid) && user.Gid != gid)
                    {
                        continue;
                    }
                    if (!string.IsNullOrEmpty(comment) && user.Comment != comment)
                    {
                        continue;
                    }
                    if (!string.IsNullOrEmpty(home) && user.Home != home)
                    {
                        continue;
                    }
                    if (!string.IsNullOrEmpty(shell) && user.Shell != shell)
                    {
                        continue;
                    }
                    users.Add(user);
                }
            }
            file.Close();
            return users;
        }

        public List<Group> GetUserGroups(string uid)
        {
            var user = GetUser(uid);
            var groups = groupsService.GetGroups();
            return groups.FindAll(group => group.Members.Contains(user.Name));
        }

        public List<User> GetUsers()
        {
            List<User> users = new List<User>();
            string line;
            StreamReader file = new StreamReader(userPath);
            while ((line = file.ReadLine()) != null)
            {
                User user = BuildUser(line);
                if (user != null)
                {
                    users.Add(user);
                }
            }
            file.Close();
            return users;
        }

        private User BuildUser(string line)
        {
            if (line.StartsWith("#", StringComparison.Ordinal))
            {
                return null;
            }
            string[] parts = line.Split(":");
            return new User
            {
                Name = parts[0],
                Uid = parts[2],
                Gid = parts[3],
                Comment = parts[4],
                Home = parts[5],
                Shell = parts[6]
            };
        }
    }
}
