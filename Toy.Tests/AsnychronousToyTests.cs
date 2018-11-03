using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using toy.Models;
using Xunit;

namespace Toy.Tests
{
    public class AsynchronousToyTests : BaseServiceTests
    {
        private List<User> retUsers = new List<User>();
        private List<Group> retGroups = new List<Group>();

        [Fact]
        public void GetGroups()
        {
            Task task1 = Task.Factory.StartNew(() => TaskGetUser("1001"));
            Task task2 = Task.Factory.StartNew(() => TaskGetUser("1001"));
            Task task3 = Task.Factory.StartNew(() => TaskGetUser("1001"));
            Task task4 = Task.Factory.StartNew(() => TaskGetUser("1001"));
            Task task5 = Task.Factory.StartNew(() => TaskGetUser("1001"));
            Task task6 = Task.Factory.StartNew(() => TaskGetUser("1001"));
            Task task7 = Task.Factory.StartNew(() => TaskGetUser("1001"));
            Task task8 = Task.Factory.StartNew(() => TaskGetUser("1001"));

            Task.WaitAll(task1, task2, task3, task4, task5, task6, task7, task8);

            Assert.True(retUsers.Count == 8);
        }

        private void TaskGetUser(string uid)
        {
            retUsers.Add(service.GetUser(uid));
        }

        private void TaskGetGroup(string gid)
        {
            retGroups.Add(groupsService.GetGroup(gid));
        }
    }
}
