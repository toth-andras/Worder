using System.Data;
using Domain.Users;
using Infrastructure;
using Infrastructure.Options;
using Infrastructure.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<PostgresOptions>(builder.Configuration.GetSection(nameof(PostgresOptions)));
builder.Services.AddScoped<IDbConnection>(provider =>
{
    var options = provider.GetRequiredService<IOptions<PostgresOptions>>().Value;
    var connetcion = new NpgsqlConnection(options.ConnectionString);
    connetcion.Open();
    return connetcion;
});
builder.Services.AddRepositories();
builder.Services.AddServices();


builder.Services.AddControllers();

var app = builder.Build();

app.MapControllers();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.Run();