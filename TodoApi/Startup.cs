using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using TodoApi.Data;
using static TodoApi.Data.DbInitializer;

namespace TodoApi
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();

        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddDbContext<TodoContext>(opt => opt.UseInMemoryDatabase());

            services.AddDbContext<TodoContext>(opt => opt.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));

            // Add framework services.
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, TodoContext dbContext)
        {
            var loggingCfg = Configuration.GetSection("Logging");

            loggerFactory.AddConsole(loggingCfg);
            loggerFactory.AddDebug();

            loggerFactory.AddFile(loggingCfg);

            app.UseMvc();

            DbInitializer.Initialize(dbContext);
        }
    }
}
