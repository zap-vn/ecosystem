using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using ZAP.Ecosystem.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddOpenApi();

builder.Services.AddDbContext<EcosystemDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("ZAP_Ecosystem"));
});

// Configure Generic Repository from Shared Module
builder.Services.AddScoped(typeof(ZAP.Ecosystem.Shared.Data.IBaseRepository<>), typeof(ZAP.Ecosystem.Shared.Data.BaseRepository<>));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.Run();
