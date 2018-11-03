using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using toy.Controllers;
using toy.Services;

namespace toy
{
    public class Startup
    {
        public string UsersFilePath = @"/etc/passwd";
        public string GroupFilePath = @"/etc/group";

        private readonly IConfiguration _config;

        public Startup(IConfiguration config)
        {
            _config = config;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            CheckFiles();
            
            services.AddMvc();
            services.AddTransient<IGroupsService, GroupsService>((ctx) =>
            {
                return new GroupsService(GroupFilePath);
            });
            services.AddTransient<IUsersService, UsersService>((ctx) => {
                var groupsService = new GroupsService(GroupFilePath);
                return new UsersService(UsersFilePath, groupsService);
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Toy HTTP API", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Toy API v1");
            });
        }

        private void CheckFiles()
        {
            var userPath = _config.GetValue<string>("usersPath");
            var groupPath = _config.GetValue<string>("groupsPath");

            if (!string.IsNullOrEmpty(userPath))
            {
                UsersFilePath = userPath;
            }
            if (!string.IsNullOrEmpty(groupPath))
            {
                GroupFilePath = groupPath;
            }

            if (!File.Exists(UsersFilePath))
            {
                throw new FileNotFoundException($"The input file: {UsersFilePath} passed in was not found");
            }
            if (!IsUsersMalformed(UsersFilePath))
            {
                throw new ArgumentException($"The input file: {UsersFilePath} passed in was not in the correct format.");
            }
            if (!File.Exists(GroupFilePath))
            {
                throw new FileNotFoundException($"The input file: {GroupFilePath} passed in was not found");
            }
            if (!IsGroupsMalformed(GroupFilePath))
            {
                throw new ArgumentException($"The input file: {GroupFilePath} passed in was not in the correct format.");
            }
        }

        private bool IsGroupsMalformed(string filePath)
        {
            string line;
            StreamReader file = new StreamReader(filePath);
            while ((line = file.ReadLine()) != null)
            {
                if (line.StartsWith("#", StringComparison.Ordinal))
                {
                    continue;
                }
                if (line.Count(f => f == ':') != 3)
                {
                    return false;
                }
                var parts = line.Split(":");
                int x;
                if (!Int32.TryParse(parts[2], out x))
                {
                    return false;
                }
            }
            file.Close();
            return true;
        }

        private bool IsUsersMalformed(string filePath)
        {
            string line;
            StreamReader file = new StreamReader(filePath);
            while ((line = file.ReadLine()) != null)
            {
                if (line.StartsWith("#", StringComparison.Ordinal))
                {
                    // this is a check for my mac, whose format starts with a few title strings starting with a #
                    continue;
                }
                if (line.Count(f => f == ':') != 6)
                {
                    return false;
                }
                var parts = line.Split(":");
                int x;
                if(!Int32.TryParse(parts[2], out x))
                {
                    return false;
                }
                if (!Int32.TryParse(parts[3], out x))
                {
                    return false;
                }
            }
            file.Close();
            return true;
        }
    }
}
