using Microsoft.OpenApi.Models;
using ScanApp2.Middleware;
using ScanApp2.Services;
using ScanApp2.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Ajouter les services nécessaires
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Injection des services
builder.Services.AddSingleton<GoogleSheetsStorageService>(); // Service Google Sheets
builder.Services.AddScoped<IScanService, ScanService>();
builder.Services.AddScoped<IUserContextService, UserContextService>();
builder.Services.AddHttpContextAccessor();

// ✅ Configurer CORS pour DEV + PROD
builder.Services.AddCors(options =>
{
    options.AddPolicy("DevAllowAll", policy =>
    {
        policy.WithOrigins(
            "http://localhost:5173", // frontend local
            "https://scanapp2-frontend-opcy.vercel.app", // domaine principal Vercel
            "https://scanapp2-frontend-opcy-7pi2vwxun-hindacharrabs-projects.vercel.app" // sous-domaine Vercel
        )
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

var app = builder.Build();

// Middleware global
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseHttpsRedirection();

// ✅ Appliquer CORS avant les contrôleurs
app.UseCors("DevAllowAll");

// Middleware et Swagger en DEV
if (app.Environment.IsDevelopment())
{
    app.UseMiddleware<TestUserMiddleware>(); // Injecte un user/site par défaut
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Mapper les controllers
app.MapControllers();

app.Run();
