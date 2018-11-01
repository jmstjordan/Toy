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
            "$_analyticsd:*:263:263:Analytics Daemon:/var/db/analyticsd:/usr/bin/false",
            "$_fpsd:*:265:265:FPS Daemon:/var/db/fpsd:/usr/bin/false",
            "$_timed:*:266:266:Time Sync Daemon:/var/db/timed:/usr/bin/false"
            };
        string[] groupLines = {
            $"_analyticsd:*:263:_analyticsd",
            $"_fpsd:*:265:_fpsd",
            $"_timed:*:266:"
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
