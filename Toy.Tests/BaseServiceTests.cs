using System;
using System.IO;
using toy.Controllers;
using toy.Services;

namespace Toy.Tests
{
    public class BaseServiceTests
    {
        public string groupsFilePath;
        public string usersFilePath;

        public string[] usersLines = {
            $"federer:x:1001:1006:Greatest Tennis Player of All Time:/home/federer:/usr/bin/false",
            $"lebron:x:1002:1007::/home/lebron:/usr/bin/false",
            $"janmichael:x:1003:1008:Greatest Jan-Michael of all time:/home/janmichael:/usr/bin/false",
            $"mycat:x:1004:1009:Greatest Cat of all time:/home/mycat:"
        };
        public string[] groupLines = {
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

        public IGroupsService groupsService;
        public IUsersService service;

        public BaseServiceTests(string testPath)
        {
            groupsFilePath = "etcgroup_test_" + testPath;
            usersFilePath = "etcpasswd_test_" + testPath;
            groupsService = new GroupsService(groupsFilePath);
            service = new UsersService(usersFilePath, groupsService);

            GenerateFile(usersLines, usersFilePath);
            GenerateFile(groupLines, groupsFilePath);
        }

        private void GenerateFile(string[] lines, string filePath)
        {
            using (StreamWriter file = new StreamWriter(filePath))
            {
                foreach (string line in lines)
                {
                    file.WriteLine(line);
                }
            }
        }
    }
}