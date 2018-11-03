using System;
using System.IO;
using Xunit;

namespace Toy.Tests
{
    public class UserServiceTests : BaseServiceTests
    {
        [Fact]
        public void GetUsers()
        {
            var users = service.GetUsers();
            Assert.True(users.Count == 4);
        }

        [Fact]
        public void GetUser()
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
        public void GetUsersByQuery()
        {
            var users = service.GetUsersByQuery(null, null, null, null, null, null);
            Assert.True(users.Count == 4);
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

        [Fact]
        public void GetUsersByQueryAllFields()
        {
            var users = service.GetUsersByQuery("lebron", "1002", "1007", "", $"/home/lebron", $"/usr/bin/false");
            Assert.True(users.Count == 1);

            var usersNullComment = service.GetUsersByQuery("lebron", "1002", "1007", "", $"/home/lebron", $"/usr/bin/false");
            Assert.True(users.Count == 1);
        }
    }
}
