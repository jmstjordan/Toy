using System;
using System.Collections.Generic;
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
            var userPath = _config.GetValue<string>("usersPath");
            var groupPath = _config.GetValue<string>("groupsPath");

            if(!string.IsNullOrEmpty(userPath))
            {
                UsersFilePath = userPath;
            }
            if(!string.IsNullOrEmpty(groupPath))
            {
                GroupFilePath = groupPath;
            }

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
    }
}
