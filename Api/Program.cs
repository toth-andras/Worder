using System.Data;
using Api.Middlewares;
using FluentValidation.AspNetCore;
using Infrastructure;
using Infrastructure.Options;
using Microsoft.Extensions.Options;
using Npgsql;
using Validation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<PostgresOptions>(builder.Configuration.GetSection(nameof(PostgresOptions)));
builder.Services.AddTransient<IDbConnection>(provider =>
{
    var options = provider.GetRequiredService<IOptions<PostgresOptions>>().Value;
    var connection = new NpgsqlConnection(options.ConnectionString);
    connection.Open();
    return connection;
});
builder.Services.AddRepositories();
builder.Services.AddServices();
builder.Services.AddMiddlewares();

builder.Services.AddControllers();

builder.Services.AddApiRequestModelValidators();


var app = builder.Build();

app.MapControllers();

//app.AddDapperMappers();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddlewares();

app.UseHttpsRedirection();
app.Run();