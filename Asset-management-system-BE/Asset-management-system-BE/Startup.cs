using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Azure.Storage.Blobs;
using Asset_management_system_service;
using Asset_management_system_commonLibarary;
using Asset_management_system_DAL.Models;
using Microsoft.EntityFrameworkCore;
using Asset_management_system_DAL;

namespace Asset_management_system_BE
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
            services.AddControllers();
            services.AddCors();
            services.AddDbContext<AssetManagementSystemDBContext>(options => options.UseSqlServer("Server = localhost; Database = AssetManagementSystemDB; Trusted_Connection = True;"));
            /*Services */
            services.AddScoped<IComputerVisionClient>(factory => {
                var key = Configuration["ComputerVisionKey"];
                var host = Configuration["ComputerVisionEndpoint"];
                var test = Configuration["AzureBlobStorage"];
                var hhh = Configuration.GetConnectionString("DBConnection");
                var credentials = new ApiKeyServiceClientCredentials(key);
                var client = new ComputerVisionClient(credentials);
                client.Endpoint = host;

                return client;
            });
            services.AddScoped(x => { return (new BlobServiceClient(Configuration["AzureBlobStorage"])); });
            services.AddOptions();
            services.Configure<ConfigurationValues>(Configuration.GetSection("AppValue"));

            //Services
            services.AddScoped<IBlobService, BlobService>();
            services.AddScoped<ICognitiveService, CognitiveService>();
            services.AddScoped<IAssetService, AssetService>();
            //Repositories
            services.AddScoped<IMetadataRepository, MetadataRepository>();
            services.AddScoped<IImageVariantRepository, ImageVariantRepository>();
            services.AddScoped<IAssetRepository, AssetRepository>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors(builder => builder
               .AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader());
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
