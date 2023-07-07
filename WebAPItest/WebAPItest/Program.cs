using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using WebAPItest.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerDocument();

// �[�JCORS�w�]Policy�A���\����Origin URL�s����Web API
builder.Services.AddCors(options => // �[�JCORS�w�]Policy
 options.AddDefaultPolicy(builder => builder.AllowAnyOrigin())); // ���\����Origin 

builder.Services.AddDbContext<A01Context>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("A01Database")));

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(option =>
{
    //���n�J�ɷ|�۰ʾɨ�o�Ӻ��}
    option.LoginPath = new PathString("/api/Login/NoLogin");
});

var app = builder.Build();

/*if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}*/

// ���޶}�o���ҩγ��p�����ҳ��ϥ�Swagger���;���Swagger UI
// �W�[Swagger���;���Swagger UI�����n���Request Pipeline
app.UseOpenApi(); // �ϥ�Swagger 2.0 (OpenApi)���;������n��
app.UseSwaggerUi3(); // �ϥ� Swagger UI 3.0 �����n��


app.UseCookiePolicy();
app.UseAuthentication();
app.UseAuthorization();

app.UseCors(); //  �ϥ�CORS�����n��

app.MapControllers();

app.Run();
