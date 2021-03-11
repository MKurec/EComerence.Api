using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using EComerence.Infrastructure.Repositories;
using EComerence.Core;
using EComerence.Core.Repositories;
using EComerence.Infrastructure.Services;
using EComerence.Infrastructure.Settings;
using EComerence.Infrastructure.Mappers;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerUI;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace EComerence.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        private readonly JwtSettings _jWTSettings;
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        public Startup(IOptions<JwtSettings> jwtSeting, IConfiguration configuration)
        {
            _jWTSettings = jwtSeting.Value;
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>();
            services.AddAuthorization();
            services.AddScoped<DbContext, ApplicationDbContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IOrderListRepository, OrderListRepository>();
            services.AddScoped<IProducerRepository, ProducerRepository>();
            services.AddScoped<IFileRepository, FileRepository>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IProducerService, ProducerService>();
            services.AddScoped<IOrderListService, OrderListService>();
            services.AddMvc(options => options.EnableEndpointRouting = false);
            services.AddSingleton<IJwtHandler, JwtHandler>();
            services.Configure<JwtSettings>(Configuration.GetSection("JWT"));
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.AddControllersWithViews().AddNewtonsoftJson(options =>
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);

            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                                  builder =>
                                  {
                                      builder.WithOrigins("http://localhost:3000",
                                                          "https://localhost:3000")
                                                            .AllowAnyHeader()
                                                        .AllowAnyMethod(); ;
                                  });
            });

            services.AddSingleton(AutoMapperConfig.Initialize());
            services.AddAuthentication
                (o =>
                {
                    o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {

                    options.TokenValidationParameters = new TokenValidationParameters()
                    {

                        ValidateIssuer = true,
                        ValidIssuer = ("http://localhost:44367"),//http?
                        ValidateAudience = false,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("super_secret_password!")),
                        ClockSkew = TimeSpan.FromMinutes(5),
                        RequireSignedTokens = true
                    };
                });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Latest);
            services.AddApplicationInsightsTelemetry();
            services.AddSwaggerGen(c =>
             {
                 c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "My API", Version = "v1" });
                 var security = new Dictionary<string, IEnumerable<string>>
                 {
                     { "Bearer", new string[] { } },
                 };

                 c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                 {
                     Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                     Name = "Authorization",
                     In = ParameterLocation.Header,
                     Type = SecuritySchemeType.ApiKey,
                     Scheme = "Bearer"
                 });
                 c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                 {
                     {
                         new OpenApiSecurityScheme
                         {
                             Reference = new OpenApiReference
                             {
                                 Type = ReferenceType.SecurityScheme,
                                 Id = "Bearer"
                             },
                             Scheme = "oauth2",
                             Name = "Bearer",
                             In = ParameterLocation.Header,

                         },
                         new List<string>()
                     },
                 });
             });        
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ApplicationDbContext db)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseCors(MyAllowSpecificOrigins);


            app.UseFileServer();
            db.Database.Migrate();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;
                c.DocumentTitle = "Title Documentation";
                c.DocExpansion(DocExpansion.None);

            }); 
            app.UseAuthentication();
            app.UseMvcWithDefaultRoute();
        }
    }
}
