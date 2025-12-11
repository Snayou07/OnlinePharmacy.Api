using Microsoft.EntityFrameworkCore;
using OnlinePharmacy.Api.Data;
using OnlinePharmacy.Api.Interfaces;
using OnlinePharmacy.Api.Services;
using System.Text.Json.Serialization; // Чтобы не было ошибок с циклами

var builder = WebApplication.CreateBuilder(args);

// Добавляем контроллеры с настройкой JSON (игнорировать циклы)
builder.Services.AddControllers().AddJsonOptions(x =>
   x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<PharmacyDbContext>(options =>
    options.UseSqlite("Data Source=pharmacy.db"));

// РЕГИСТРАЦИЯ ВСЕХ СЕРВИСОВ
builder.Services.AddScoped<IMedicineService, MedicineService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IOrderService, OrderService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<PharmacyDbContext>();
    db.Database.EnsureCreated();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();