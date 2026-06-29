using Api.Practice.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddEndpointsApiExplorer()
    .AddControllers();

builder.Services
    .AddSwaggerGen()
    .AddDependencies();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});


var app = builder.Build();

app.SeedDatabase();

app.UseCors("AllowAll");
app.UseHttpsRedirection();
app.UseSwagger();
app.UseSwaggerUI();
app.UseRouting();
app.MapControllers();


app.Run();