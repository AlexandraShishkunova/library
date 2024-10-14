using libraryWeb;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using libraryWeb.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using libraryWeb.Data;

var builder = WebApplication.CreateBuilder(args);





// ��������� JWT-��������������
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,           // �������� �������� ������
        ValidateAudience = true,         // �������� ���������� ������
        ValidateLifetime = true,         // �������� ����� �������� ������
        ValidateIssuerSigningKey = true, // �������� ������� ������
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

// ����������� ��������
builder.Services.AddDbContext<LibraryContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ��������� Identity � �������������� ��������� ���� ������
builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<LibraryContext>().AddDefaultTokenProviders();


builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();

builder.Services.AddControllers(); // ���������� ��������� ������������

// ����������� Swagger (������ ���� �� ������ Build)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllersWithViews();

// �������� ����������
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// ��������� middleware (��������� ��������� ��������)
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error"); // �������� ��������� ������
    app.UseHsts();
}

app.UseHttpsRedirection(); // ��������������� �� HTTPS
app.UseStaticFiles(); // ��������� ����������� ������
app.UseRouting(); // ��������� �������������
// ���������� �������������� � �����������
app.UseAuthentication();
app.UseAuthorization();

// ��������� ���������
app.MapControllers();


// ������ ����������
app.Run();
