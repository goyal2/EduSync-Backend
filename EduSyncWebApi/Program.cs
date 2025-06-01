using Microsoft.EntityFrameworkCore;
using EduSyncWebApi.Data;
using EduSyncWebApi.Services;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("Allow", builder =>
    {
        // builder.WithOrigins("http://localhost:3000") // Your React app origin
        //        .AllowAnyHeader()
        //        .AllowAnyMethod()
        //        .AllowCredentials();

        builder.WithOrigins("https://gray-beach-076d1a300.6.azurestaticapps.net")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

// Add services to the container.
builder.Services.AddScoped<IBlobService, BlobService>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("Allow");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

