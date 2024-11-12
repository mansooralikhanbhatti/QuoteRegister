using Microsoft.EntityFrameworkCore;
using QuoteRegister.Entities;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();


builder.Services.AddDbContext<QuoteTrackingDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("QuoteTrackingDB")));

// Register Swagger services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
