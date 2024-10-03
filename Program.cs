using libraryWeb;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Регистрация сервисов
builder.Services.AddDbContext<LibraryContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers(); // Добавление поддержки контроллеров

// Регистрация Swagger (должно быть до вызова Build)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Создание приложения
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Настройка middleware (конвейера обработки запросов)
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error"); // Страница обработки ошибок
    app.UseHsts();
}

app.UseHttpsRedirection(); // Перенаправление на HTTPS
app.UseStaticFiles(); // Поддержка статических файлов
app.UseRouting(); // Настройка маршрутизации
app.UseAuthorization(); // Поддержка авторизации

// Настройка маршрутов
app.MapControllers();

// Запуск приложения
app.Run();
