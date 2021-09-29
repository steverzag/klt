using FloraYFaunaAPI.Context;
using FloraYFaunaAPI.Exceptions;
using FloraYFaunaAPI.Models;
using FloraYFaunaAPI.Services;
using FloraYFaunaAPI.Services.Contract;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;

namespace FloraYFaunaAPI
{
    public class Startup
    {
        private static IConfigurationRoot Configuration { get; set; }
        private IWebHostEnvironment HostingEnvironment { get; set; }
        private AppSettingsModel AppSettings { get; set; }

        public Startup(IWebHostEnvironment env)
        {
            HostingEnvironment = env;

            var builder = new ConfigurationBuilder()
              .SetBasePath(env.ContentRootPath)
              .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true) 
              .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
              .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            DbContextOptions dbContextOptions = null;
            var configurationSection = Configuration.GetSection("ConnectionStrings:DefaultConnection");
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configurationSection.Value);
                dbContextOptions = options.Options;
            });

            services.AddHttpContextAccessor();
            services.AddResponseCaching(x =>
            {
                x.MaximumBodySize = 1024;
                x.UseCaseSensitivePaths = false;
            })
            .AddScoped<IJwtUtils, JwtUtils>()
            .AddScoped<IUserServices, UserServices>()
            .AddTransient<ITelegramBotServices, TelegramBotServices>()
            .AddAntiforgery(delegate (AntiforgeryOptions options)
            {
                options.HeaderName = "X-XSRF-TOKEN";
                options.Cookie.Name = "XSRF";
            });
  
            services.Configure<IISOptions>(options =>{ options.ForwardClientCertificate = false; })
            .AddAutoMapper(config => {
                AutomapperStartup.Config(config);
            }, Assembly.GetEntryAssembly());

            var SettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettingsModel>(SettingsSection);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => {
               options.TokenValidationParameters = new TokenValidationParameters()
               {
                   ValidateIssuer = true,
                   ValidateAudience = true,
                   ValidateLifetime = true,
                   ValidateIssuerSigningKey = true,
                   ValidIssuer = SettingsSection["Jwt:ISSUER_TOKEN"],
                   ValidAudience = SettingsSection["Jwt:AUDIENCE_TOKEN"],
                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SettingsSection["Jwt:SECRET_KEY"]))
               };
           });

            services.AddControllers().AddNewtonsoftJson();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Flora Y Fauna", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,ApplicationDbContext dBContext,IOptions<AppSettingsModel> appSettingsAccessor)
        {
            AppSettings = appSettingsAccessor.Value;
            DemoDataGenerator.SeedData(dBContext);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Flora Y Fauna v1"));
            }

            app.Use(async (context, next) => {

                string path = context.Request.Path.Value;
                if (path != null && !path.ToLower().Contains("/api"))
                {
                    // XSRF-TOKEN used by angular in the $http if provided
                    
                }

                await next();
            });

            app.UseStaticFiles();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(x => x
               .SetIsOriginAllowed(origin => true)
               .AllowAnyMethod()
               .AllowAnyHeader()
               .AllowCredentials());

            app.UseMiddleware<ErrorHandlerMiddleware>();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
