﻿using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository;
using Service;
using Serilog;
using API_Architecture.Middelware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<AppDbContext>(context =>
{
    context.UseSqlServer(builder.Configuration.GetConnectionString("Default"),
    opt => opt.MigrationsAssembly("Repository"));
});

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration).CreateLogger();

builder.Services.AddRepositoryLayer();
builder.Services.AddServiceLayer();
builder.Host.UseSerilog();

builder.Services.AddTransient<GlobalExceptionHandlingMiddleware>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging(); 

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

