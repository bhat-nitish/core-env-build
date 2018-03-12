using EnvBuild.Config;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace EnvBuild
{
    public class Startup
    {
        public static string[] args { get; set; }

        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            // override config with build params here - anything not supplied via build params will be defaulted to values in appsettings.json
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                 .AddEnvironmentVariables()
                .AddCommandLine(args);
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<GitConfig>(Configuration.GetSection("GitConfig"));
            services.Configure<AuthConfig>(Configuration.GetSection("AuthConfig"));
            // map the data from config file to an in memory class object which can be injected, this will make it easier to read data mapped to sections in config via models
            services.AddScoped(cfg => cfg.GetService<IOptionsSnapshot<GitConfig>>().Value);
            services.AddScoped(cfg => cfg.GetService<IOptionsSnapshot<AuthConfig>>().Value);

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
