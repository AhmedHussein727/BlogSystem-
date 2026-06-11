
using Blog.Domain.Entities;
using Blog.Domain.Interfaces;
using Blog.Presistance.Data.DataSeed;
using Blog.Presistance.Data.DbContexts;
using Blog.Presistance.Rebositories;
using Blog.Services.Abstraction;
using Blog.Sevices;
using Blog.Sevices.MappingProfiles;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace BlogSystem.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);



            // Add services to the container.
            builder.Services.AddDbContext<BlogDbContext>(options =>
            {
                options.UseNpgsql(
                    builder.Configuration.GetConnectionString("DefaultConnection")
                );
            });

            builder.Services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<BlogDbContext>()
                .AddDefaultTokenProviders();

           

            builder.Services.AddScoped<IAuthenticationService, AuthenticationService >();
            builder.Services.AddScoped<IDataIntializer, IdentityDataIntializer>();
            builder.Services.AddAutoMapper(typeof(ServiceAssemblyReference).Assembly);
            builder.Services.AddScoped<IBlogPostsService, BlogPostsService>();
            builder
               .Services.AddAuthentication(options =>
               {
                   options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                   options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
               })
               .AddJwtBearer(options =>
               {
                   options.SaveToken = true;
                   options.TokenValidationParameters = new TokenValidationParameters()
                   {
                       ValidateIssuer = true,
                       ValidateAudience = true,
                       ValidateLifetime = true,
                       ValidIssuer = builder.Configuration["JWTOptions:Issuer"],
                       ValidAudience = builder.Configuration["JWTOptions:Audience"],
                       IssuerSigningKey = new SymmetricSecurityKey(
                           Encoding.UTF8.GetBytes(builder.Configuration["JWTOptions:SecretKey"]!)
                       ),
                   };
               });
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<ICommentService,CommentService>();
            builder.Services.AddScoped<ICatService,CatService>();
            builder.Services.AddControllers();
            builder.Services.AddScoped<IDashboardService, DashboardService>();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            using var scope = app.Services.CreateScope();

            var dataIntializer = scope.ServiceProvider.GetRequiredService<IDataIntializer>();

            dataIntializer.InitializeAsync().GetAwaiter().GetResult();

           

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();


            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
