using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using HackCovidAPICore.DataAccess;
using AutoMapper;
using System;

namespace HackCovidAPICore
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
			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
			services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
			services.AddSingleton<INoteCosmosDB>(x => new NoteCosmosDB(Configuration.GetConnectionString("NoteCosmosDB")));
			services.AddSingleton<IUserCosmosDB>(x=> new UserCosmosDB(Configuration.GetConnectionString("UserCosmosDB")));
			services.AddSingleton(typeof(IPushNotificationService), typeof(PushNotificationService));
			services.AddCors();
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
				//app.UseHsts();
			}

			//app.UseHttpsRedirection();
			app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
			app.UseAuthentication();
			app.UseMvc();
		}
	}
}
