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
                $"_dev:*:1:-1:Developer Documentation:/var/empty:/usr/bin/false",
                $"_csv:*:2:263:CSV Server:/var/empty:/usr/bin/false",
                $"_app:*:3:250:Application Server:/var/empty:/usr/bin/false"
            };
        string[] groupLines = {
                $"_analyticsusers:*:250:_analyticsd,_networkd,_timed",
                $"_analyticsd:*:263:_analyticsd"
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

            

            File.Delete(usersFilePath);
            File.Delete(groupsFilePath);
        }
    }
}
