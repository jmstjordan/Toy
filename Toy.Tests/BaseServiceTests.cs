using System;
using System.IO;
using toy.Controllers;
using toy.Services;

namespace Toy.Tests
{
    public class BaseServiceTests
    {
        public string groupsFilePath = @"../../../testfiles/group_test";
        public string usersFilePath = @"../../../testfiles/passwd_test";

        public IGroupsService groupsService;
        public IUsersService service;

        public BaseServiceTests()
        {
            groupsService = new GroupsService(groupsFilePath);
            service = new UsersService(usersFilePath, groupsService);
        }
    }
}