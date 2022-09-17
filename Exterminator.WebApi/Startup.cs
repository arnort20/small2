using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Exterminator.Repositories.Data;
using Exterminator.Repositories.Implementations;
using Exterminator.Repositories.Interfaces;
using Exterminator.Services.Implementations;
using Exterminator.Services.Interfaces;

namespace Exterminator.WebApi
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
            services.AddDbContext<LogDbContext>(options => 
            {
                options.UseSqlite(Configuration.GetConnectionString("LogDbConnectionString"), 
                    b => b.MigrationsAssembly("Exterminator.WebApi"));
            });

            RegisterDependencies(services);
            services.AddControllers().AddJsonOptions(options => {
                
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Exterminator.WebApi", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Exterminator.WebApi v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void RegisterDependencies(IServiceCollection services)
        {
            // Services
            services.AddTransient<ILogService, LogService>();
            services.AddTransient<IGhostbusterService, GhostbusterService>();


            // Repositories
            services.AddTransient<ILogRepository, LogRepository>();
            services.AddTransient<IGhostbusterRepository, GhostbusterRepository>();

            // Other
        }
    }
}
