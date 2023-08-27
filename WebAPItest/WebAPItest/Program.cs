using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using System.Text;
using WebAPItest;
using WebAPItest.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("appsettings.json");

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerDocument();

// 加入CORS預設Policy，允許任何Origin URL存取此Web API
builder.Services.AddCors(options => // 加入CORS預設Policy
 options.AddDefaultPolicy(builder => builder.AllowAnyOrigin())); // 允許任何Origin 

builder.Services.AddDbContext<A01Context>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("A01Database")));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = "https://securetoken.google.com/ar-home-design";
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = "https://securetoken.google.com/ar-home-design",
            ValidateAudience = true,
            ValidAudience = "ar-home-design",
            ValidateLifetime = true
        };
    });

var app = builder.Build();

/*if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}*/

// 不管開發環境或部署的環境都使用Swagger產生器及Swagger UI
// 增加Swagger產生器及Swagger UI中介軟體到Request Pipeline
app.UseOpenApi(); // 使用Swagger 2.0 (OpenApi)產生器中介軟體
app.UseSwaggerUi3(); // 使用 Swagger UI 3.0 中介軟體



app.UseCookiePolicy();
app.UseAuthentication();
app.UseAuthorization();

app.UseCors(); //  使用CORS中介軟體

app.MapControllers();

app.Run();
