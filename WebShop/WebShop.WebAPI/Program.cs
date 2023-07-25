using Microsoft.EntityFrameworkCore;
using MyShop.Persistence.DbContexts;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using WebShop.Application.Common.Mappings;
using WebShop.Application.DependencyInjection;
using WebShop.Persistence.DependencyInjection;
using WebShop.Application.Common.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// get configuration

var configuration = builder.Configuration;


// Add services to the container.

// Add Clean-Architecture layers
builder.Services.AddApplication();
builder.Services.AddPersistence(configuration);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add automapper
builder.Services.AddAutoMapper(config => {
    config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
    config.AddProfile(new AssemblyMappingProfile(typeof(IApplicationDbContext).Assembly));
});



// enable CORS to all sources
builder.Services.AddCors(options => {
    options.AddPolicy("AllowAll", policy => {
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
        policy.AllowAnyOrigin();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if ( app.Environment.IsDevelopment() ) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.UseCors("AllowAll");

app.Run();
