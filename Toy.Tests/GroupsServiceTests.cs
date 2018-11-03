using System;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace Toy.Tests
{
    public class GroupsServicesTests : BaseServiceTests
    {
        [Fact]
        public void GetGroups()
        {
            var groups = groupsService.GetGroups();
            Assert.True(groups.Count == 9);
        }

        [Fact]
        public void GetGroup()
        {
            var engineer = groupsService.GetGroup("1004");
            Assert.Equal($"engineer", engineer.Name);
            Assert.Equal($"1004", engineer.Gid);
            Assert.Contains("janmichael", engineer.Members);
        }

        [Fact]
        public void GetGroupsByQuery()
        {
            var groups = groupsService.GetGroupsByQuery(null, null, null);
            Assert.True(groups.Count == 9);
        }

        [Fact]
        public void GetGroupsByQueryName()
        {
            var groups = groupsService.GetGroupsByQuery("swiss", null, null);
            Assert.True(groups.Count == 1);
            Assert.True(groups[0].Name == "swiss");
        }

        [Fact]
        public void GetGroupsByQueryMembers()
        {
            var groups = groupsService.GetGroupsByQuery(null, null, new List<string>{"lebron"});
            Assert.True(groups.Count == 2);
            Assert.True(groups.Find(group => group.Name == "usa") != null);
            Assert.True(groups.Find(group => group.Name == "athlete") != null);
        }

        [Fact]
        public void GetGroupsByQueryMembersMultiple()
        {
            var groups = groupsService.GetGroupsByQuery(null, null, new List<string> { "lebron", "federer" });
            Assert.True(groups.Count == 1);
            Assert.True(groups[0].Name == "athlete");
        }

        [Fact]
        public void GetGroupsByQueryTooManyMembers()
        {
            var groups = groupsService.GetGroupsByQuery(null, null, new List<string> { "lebron", "federer", "janmichael" });
            Assert.True(groups.Count == 0);
        }

        [Fact]
        public void GetGroupsByQueryNameAndMember()
        {
            var groups = groupsService.GetGroupsByQuery(null, "1002", new List<string> { "lebron" });
            Assert.True(groups.Count == 1);
            Assert.True(groups[0].Name == "usa");
        }

        [Fact]
        public void GetGroupsByQueryEmptyMembers()
        {
            var groups = groupsService.GetGroupsByQuery(null, "1005", null);
            Assert.True(groups.Count == 1);
            Assert.True(groups[0].Name == "lawyer");
        }

        [Fact]
        public void GetGroupsByQueryAllFields()
        {
            var groups = groupsService.GetGroupsByQuery("swiss", "1001", new List<string> { "federer" });
            Assert.True(groups.Count == 1);
            Assert.True(groups[0].Name == "swiss");
        }
    }
}
