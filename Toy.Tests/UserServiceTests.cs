using System;
using System.IO;
using toy.Controllers;
using toy.Services;
using Xunit;

namespace Toy.Tests
{
    public class UserServiceTests : BaseServiceTests, IDisposable
    {
        private IUsersService service;
        string groupsFilePath = "etcgroup_test";
        string usersFilePath = "etcpasswd_test";
        string[] usersLines = {
            $"federer:x:1001:1006:Greatest Tennis Player of All Time:/home/federer:/usr/bin/false",
            $"lebron:x:1002:1007::/home/lebron:/usr/bin/false",
            $"janmichael:x:1003:1008:Greatest Jan-Michael of all time:/home/janmichael:/usr/bin/false",
            $"mycat:x:1004:1009:Greatest Cat of all time:/home/mycat:"
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

            GenerateFile(usersLines, usersFilePath);
            GenerateFile(groupLines, groupsFilePath);
        }

        [Fact]
        public void GetUsersTest()
        {
            var users = service.GetUsers();
            Assert.True(users.Count == 4);
        }

        [Fact]
        public void GetUserTest()
        {
            var fed = service.GetUser("1001");
            Assert.Equal($"federer", fed.Name);
            Assert.Equal($"1006", fed.Gid);
            Assert.Equal($"Greatest Tennis Player of All Time", fed.Comment);
            Assert.Equal($"/home/federer", fed.Home);
            Assert.Equal($"/usr/bin/false", fed.Shell);
        }

        [Fact]
        public void GetUserGroups()
        {
            var jmGroups = service.GetUserGroups("1003");
            Assert.True(jmGroups.Count == 2);
            Assert.True(jmGroups.Find(group => group.Name == "usa") != null);
            Assert.True(jmGroups.Find(group => group.Name == "engineer") != null);

            var myCatGroups = service.GetUserGroups("1004");
            Assert.True(myCatGroups.Count == 0);
        }

        [Fact]
        public void GetUsersByQueryName()
        {
            var users = service.GetUsersByQuery("federer", null, null, null, null, null);
            Assert.True(users.Count == 1);
            Assert.True(users[0].Name == "federer");
        }

        [Fact]
        public void GetUsersByQueryNameAndShell()
        {
            var users = service.GetUsersByQuery("federer", null, null, null, null, $"/usr/bin/false");
            Assert.True(users.Count == 1);
            Assert.True(users[0].Name == "federer");
        }

        [Fact]
        public void GetUsersByQueryNameAndComment()
        {
            var users = service.GetUsersByQuery("mycat", null, null, "Greatest Cat of all time", null, null);
            Assert.True(users.Count == 1);
            Assert.True(users[0].Name == "mycat");
        }

        [Fact]
        public void GetUsersByQueryShell()
        {
            var users = service.GetUsersByQuery(null, null, null, null, null, $"/usr/bin/false");
            Assert.True(users.Count == 3);
        }

        public void Dispose()
        {
            File.Delete(usersFilePath);
            File.Delete(groupsFilePath);
        }
    }
}
