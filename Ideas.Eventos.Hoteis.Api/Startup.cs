using ExemploLogCore.ExtensionLogger;
using Hangfire;
using Ideas.Eventos.Hoteis.Jobs;
using Ideas.Eventos.Hoteis.Core.Interfaces;
using Ideas.Eventos.Hoteis.Core.Repository;
using Ideas.Eventos.Hoteis.Crawler;
using Ideas.Eventos.Hoteis.Upload;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System.IO.Compression;

namespace Ideas.Eventos.Hoteis.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        private readonly string ConnectDev = "data source=ideasfractal-homolog.database.windows.net;initial catalog=IdeasEvent;persist security info=true;user id=ideasuserdb;password=8-@@NdCPbKwMT~V/"; 
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IBasicInfoRepository, BasicInfoRepository>();
            services.AddScoped<IFullInfoRepository, FullInfoRepository>();

            services.AddScoped<ICrawlerService, CrawlerService>();
            services.AddScoped<IJobsService, JobsService>();
            services.AddScoped<IUploadService, UploadService>();

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.Configure<GzipCompressionProviderOptions>(
                options => options.Level = CompressionLevel.Optimal);
            services.AddResponseCompression(options =>
            {
                options.Providers.Add<GzipCompressionProvider>();
            });

            
            services.AddHangfire(x => x.UseSqlServerStorage(ConnectDev));
            // Register the Swagger generator, defining 1 or more Swagger documents
                services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Ideas Fractal - Buscador de hotéis", Version = "v1" });
                });
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app,  
                              IHostingEnvironment env,
                              ILoggerFactory loggerFactory)
        {
            loggerFactory.AddContext(LogLevel.Error);
            app.UseCors("CorsPolicy");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseResponseCompression();
            app.UseHttpsRedirection();
            app.UseMvc();
            app.UseHangfireServer();
            app.UseHangfireDashboard();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

        }
    }
}
