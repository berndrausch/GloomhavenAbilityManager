using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Threading.Tasks;
using GloomhavenAbilityManager.DataAccess.Contracts.interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using GloomhavenAbilityManager.Logic.Contracts.Data;
using GloomhavenAbilityManager.DataAccess.Csv;
using GloomhavenAbilityManager.DataAccess.Contracts.Interfaces;
using GloomhavenAbilityManager.Logic.Contracts.Interfaces;
using GloomhavenAbilityManager.Logic;

namespace GloomhavenAbilityManager.Blazor
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddSingleton<IFileSystem, FileSystem>();
            services.AddSingleton<ICsvConfiguration>(new CsvConfiguration(GetCsvDataDir()));
            services.AddSingleton<IAbilityCardRepository, AbilityCardRepositoryCsv>();
            services.AddSingleton<ICharacterRepository,CharacterRepositoryCsv>();
            services.AddSingleton<ICharacterClassRepository, CharacterClassRepositoryCsv>();
            services.AddSingleton<IAbilityCardService, AbilityCardService>();
            services.AddSingleton<ICharacterClassService, CharacterClassService>();
            services.AddSingleton<ICharacterService, CharacterService>();
        }

        private string GetCsvDataDir()
        {
            string csvDataDir = Configuration["CsvDataDir"];
            if (Directory.Exists(csvDataDir))
            {
                return csvDataDir;
            }
            string assemblyLocation = Assembly.GetExecutingAssembly().Location;
            return Path.Combine(Path.GetDirectoryName(assemblyLocation), csvDataDir);
        }
       
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
