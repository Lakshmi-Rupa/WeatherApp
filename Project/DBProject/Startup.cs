using DBProject.Data;
using DBProject.Extensions;
using DBProject.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System.Net;

namespace DBProject
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
            Log.Information("Configuring services....", this);
            services.Configure<ApplicationData>(conf =>
            {
                conf.OWMApiKey = Configuration["OpenWeatherApi:ApiKey"];
                conf.OWMUrl = Configuration["OpenWeatherApi:Url"];
                conf.OWMForecastUrl = Configuration["OpenWeatherApi:ForecastUrl"];
            });
            services.AddControllersWithViews();
            services.AddControllers();
            services.AddApplicationServices();
            services.AddCors(conf => conf.AddPolicy("DevCorsPolicy", conf => conf.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader().AllowCredentials()));
            services.AddHttpClient();
            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });
            services.Configure<FormOptions>(o =>
            {
                o.ValueLengthLimit = int.MaxValue;
                o.MultipartBodyLengthLimit = int.MaxValue;
                o.MemoryBufferThreshold = int.MaxValue;
            });

            var connectionString = Configuration.GetConnectionString("WeatherDbConnection");
            var builder = new SqlConnectionStringBuilder(connectionString);
            connectionString = builder.ConnectionString;

            services.AddDbContext<WeatherDbContext>(
                options => options.UseSqlServer(connectionString),
                ServiceLifetime.Transient,
                ServiceLifetime.Transient
            );
            services.AddDistributedSqlServerCache(options =>
            {
                options.ConnectionString = connectionString;
                options.SchemaName = "dbo";
                //options.TableName = "WEB_CACHE";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            Log.Information("Configuring....", this);
            app.UseForwardedHeaders();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseCors("DevCorsPolicy");
            }
            app.UseExceptionHandler(errors =>
            {
                errors.Run(async ctx =>
                {
                    Log.Error($"Internal server error while trying to complete request from {ctx.Request.GetEncodedUrl()}");

                    ctx.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                    ctx.Response.ContentType = "application/json";

                    var contextFeature = ctx.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                            await ctx.Response.WriteAsync(new ExceptionInfo()
                        {
                            StatusCode = ctx.Response.StatusCode,
                            Message = "Internal Server Error."
                        }.ToString());
                    }
                });
            });

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                   name: "api",
                   pattern: "{controller}/{id?}");
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}
