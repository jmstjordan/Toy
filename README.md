## Toy Passwd Service

### Introduction

This app is an ASP.NET Core implementation of an HTTP Service that exposes the user and group information of a UNIX-like system.

### Getting Started

To use this app, you must first install the dotnet-core 2.1 SDK:

https://www.microsoft.com/net/download/dotnet-core/2.1

Then, after cloning this repository, navigate into the Toy directory (where Toy.csproj lives, so one level down from the root), and run the following command:

```sh
dotnet run
```

This will expose the user and group information for the default path, which is /etc/passwd and /etc/group.

To use different different files, pass them in as command line arguments using /usersPath and /groupsPath:

```sh
dotnet run /usersPath=<ABSOLUTE_PATH_TO_USERS_FILE> /groupsPath=<ABSOLUTE_PATH_TO_GROUPS_FILE>
```

### File Format

If you are going to use custom files, they must somewhat match the format of /etc/group for groups and /etc/passwd for users. I simply verify that the fields are separated by colons (:), and that Uid and Gids parse to integers. For more information on this format, Google the format of /etc/passwd and /etc/group. Or visualize it yourself:

```sh
cat /etc/passwd
cat /etc/group
```

Or, you can look at my test files in Toy.Tests/testfiles

### Swagger

This application uses swagger to make use easier. Navigate to http://localhost:5000/swagger to use the SwaggerUI.

### Testing

To run the tests, navigate to Toy.Tests and run the following command:

```sh
dotnet test
```

### API Endpoints

#### GET /users
Return a list of all users on the system, as defined in the users file.

#### GET /users/query[?name=<nq>][&uid=<uq>][&gid=<gq>][&comment=<cq>][&home=< hq>][&shell=<sq>]
Return a list of users matching all of the specified query fields.

#### GET /users/<uid>
Return a single user with <uid>.

#### GET /groups
Return a list of all groups on the system, a defined by the groups file

#### GET /groups/query[?name=<nq>][&gid=<gq>][&member=<mq1>[&member=<mq2>][&. ..]]
Return a list of groups matching all of the specified query fields. 

#### GET /groups/<gid>
Return a single group with <gid>.