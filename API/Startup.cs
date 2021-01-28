using System;
using System.Text;
using API.Middleware;
using Application.Interfaces;
using Application.Security;
using Application.Shared;
using Application.Trails;
using Data;
using Domain.Entities;
using Domain.Interfaces;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NETCore.MailKit.Extensions;
using NETCore.MailKit.Infrastructure.Internal;

namespace API
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
            services.AddDbContext<hhDbContext>(opts =>
            {
                opts.EnableDetailedErrors();
                opts.UseNpgsql(Configuration.GetConnectionString("dbConnection.dev"));
            });

            services.AddIdentity<User, Role>(options =>
            {
                options.SignIn.RequireConfirmedEmail = true;
            })
            .AddEntityFrameworkStores<hhDbContext>()
            .AddSignInManager<SignInManager<User>>()
            .AddDefaultTokenProviders();

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:TokenKey"])); // TokenKey stored in appsettings.json for development. Will need to be stored on server in Production.
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opt =>
                {
                    opt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = key,
                        ValidateAudience = false,
                        ValidateIssuer = false,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero
                    };
                });

            services.AddCors(opt =>
            {
                opt.AddPolicy("CorsPolicy", policy =>
                {
                    policy.AllowAnyHeader()
                        .AllowAnyMethod()
                        .WithExposedHeaders("WWW-Authenticate")
                        .WithOrigins("https://localhost:5001")
                        .AllowCredentials();
                });
            });

            services.AddMediatR(typeof(List.Handler).Assembly);

            var mailKitOptions = Configuration.GetSection("SmtpEthereal").Get<MailKitOptions>();
            services.AddMailKit(config =>
            {
                var options = new MailKitOptions();
                config.UseMailKit(mailKitOptions);
            });

            services.AddControllers().AddFluentValidation(config =>
            {
                config.RegisterValidatorsFromAssemblyContaining<List>();
            });

            services.AddScoped<ITrailsService, TrailsService>();
            services.AddScoped<IUserAccessor, UserAccessor>();
            services.AddScoped<IEntity, BaseEntity>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<ITokenGenerator, TokenGenerator>();

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
                options.CustomSchemaIds(x => x.FullName);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<ErrorHandlingMiddleware>();

            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage(); -- commented out so that I get the errors from ErrorHandlingMiddleware class instead.
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("CorsPolicy");

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
