using AspNetApiApp.Applications.Services;
using AspNetApiApp.Domain.Interfaces;
using AspNetApiApp.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Configure services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ArticleManagementService>();
builder.Services.AddScoped<IArticleRepository, ArticleRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline for development environment
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Middleware configuration
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();