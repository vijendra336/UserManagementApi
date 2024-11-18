using Microsoft.EntityFrameworkCore;
using System.Net;
using UserManagementApi.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.Listen(IPAddress.Any, 5000); // HTTP on port 5000
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowSpecificOrigin",
//        policy => policy.WithOrigins("https://localhost:3000") // Your Next.js app's URL
//                        .AllowAnyMethod()
//                        .AllowAnyHeader());
//});
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", policy =>
        policy.WithOrigins("http://localhost:3000") // Next.js app's URL (ensure it's `http` here)
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials());
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowSpecificOrigin");

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
