using System;
using System.IO;
using toy.Controllers;
using toy.Services;
using Xunit;

namespace Toy.Tests
{
    public class UserServiceTests : BaseServiceTests
    {
        private IUsersService service;
        string groupsFilePath = "etcgroup_test";
        string usersFilePath = "etcpasswd_test";
        string[] usersLines = {
            "$federer:x:1001:1006::/home/federer:",
            "$lebron:x:1002:1007::/home/lebron:",
            "$janmichael:x:1003:1008::/home/janmichael:",
            "$mycat:x:1004:1009::/home/mycat:"
        };
        string[] groupLines = {
            $"swiss:x:1001:federer",
            $"usa:x:1002:lebron,janmichael",
            $"athlete:x:1003:federer,lebron",
            $"engineer:x:1004:janmichael",
            $"lawyer:x:1005:",
            $"federer:x:1006:",
            $"lebron:x:1007:",
            $"janmichael:x:1008:",
            $"mycat:x:1009:"
        };

        public UserServiceTests()
        {
            IGroupsService groupsService = new GroupsService(groupsFilePath);
            service = new UsersService("etcpasswd_test", groupsService);
        }
        [Fact]
        public void GetUsersTest()
        {
            GenerateFile(usersLines, usersFilePath);

            var users = service.GetUsers();
            Assert.True(users.Count == 3);

            File.Delete(usersFilePath);
        }

        [Fact]
        public void GetUserTest()
        {
            GenerateFile(usersLines, usersFilePath);

            var user = service.GetUser("1");
            Assert.Equal($"_dev", user.Name);
            Assert.Equal($"-1", user.Gid);
            Assert.Equal($"Developer Documentation", user.Comment);
            Assert.Equal($"/var/empty", user.Home);
            Assert.Equal($"/usr/bin/false", user.Shell);

            File.Delete(usersFilePath);
        }

        [Fact]
        public void GetUserGroups()
        {
            GenerateFile(usersLines, usersFilePath);
            GenerateFile(usersLines, groupsFilePath);

            var groups = service.GetUserGroups("3");
            Assert.True(groups.Count == 1);
            Assert.Equal("_analyticsusers", groups[0].Name);

            File.Delete(usersFilePath);
            File.Delete(groupsFilePath);
        }
    }
}
