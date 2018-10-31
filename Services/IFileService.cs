using System;
using System.Collections.Generic;
using toy.Models;

namespace toy.Services
{
    public interface IFileService
    {
        List<User> GetUsers();
        List<Group> GetGroups();
    }
}
