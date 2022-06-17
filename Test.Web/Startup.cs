using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Test.Core.Entities;
using Test.Infrastructure;
using Test.Repositories;
using Test.Repositories.Interfaces;
using Test.Services.Business;
using Test.Services.Interfacess;
using Test.Web.Configuration;

namespace Test.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapperConfiguration();

            services.AddJwtBearerConfiguration(Configuration);

            services.AddScoped<IJwtTokenService, JwtTokenService>();

            services.AddScoped<IRepository<Document>, DocumentReposetory>();

            services.AddScoped<IRepository<DocumentTemplate>, DocumentTemplateReposetory>();

            services.AddScoped<IDocumentService, DocumentService>();

            services.AddScoped<IUserService, UserService>();

            services.AddControllers();

            services.AddDbContext<ApplicationDbContext>(opt => opt.UseInMemoryDatabase(databaseName: "Test"));

            services.AddIdentity<ApplicationUser, IdentityRole<long>>(options =>
            {
                options.SignIn.RequireConfirmedEmail = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.SignIn.RequireConfirmedAccount = false;
                options.Password.RequireDigit = false;
            }).AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Test application", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme.",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Scheme = "bearer",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
                            },
                            new List<string>()
                        }
                    });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Test"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
