using libraryWeb;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// ����������� ��������
builder.Services.AddDbContext<LibraryContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers(); // ���������� ��������� ������������

// ����������� Swagger (������ ���� �� ������ Build)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
app.UseAuthorization(); // ��������� �����������

// ��������� ���������
app.MapControllers();

// ������ ����������
app.Run();
