﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoordinatorTaskProject.Data;
using CoordinatorTaskProject.Hubs;
using CoordinatorTaskProject.Models.AmsTables;
using CoordinatorTaskProject.Models.CoordinatorDb;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CoordinatorTaskProject
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AmsDataContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("amsConn")));
            services.AddDbContext<CoordinatorContext>(opt => opt.UseMySql(Configuration.GetConnectionString("dbConn")));
            services.AddScoped<ITaskRepository, TaskRepository>();
            services.AddSignalR();
            services.AddCors();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2).AddJsonOptions(opt => 
            opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseSignalR(routes =>
           {
               routes.MapHub<DataHub>("/taskHub");

               });
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().AllowCredentials());
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
