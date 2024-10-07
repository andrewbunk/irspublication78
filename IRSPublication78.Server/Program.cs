using IRSPublication78.Server;
using IRSPublication78.Server.Services;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<PubContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("IrsPublication78")));
builder.Services.AddTransient<OrganizationService>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

//using var scope = app.Services.CreateScope();

//var services = scope.ServiceProvider;

//var initialiser = services.GetRequiredService<DbInitializer>();
app.Run();
