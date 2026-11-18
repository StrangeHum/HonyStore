using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        // Включает удобную панель ввода Bearer-токена прямо в интерфейсе
        options.AddPreferredSecuritySchemes();

        options.WithTitle("HonyStore API Documentation");
    });
}

//app.UseHttpsRedirection();

app.MapControllers();

app.Run();