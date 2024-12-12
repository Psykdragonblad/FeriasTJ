using FeriasTJBase.Application.Interface;
using FeriasTJBase.Application.Mappings;
using FeriasTJBase.Application.Services;
using FeriasTJBase.Domain.Interface;
using FeriasTJBase.Infra.Configurations;
using FeriasTJBase.Infra.Messaging;
using FeriasTJBase.Infra.Repositories;
using FeriasTJBase.Infra.Repositories.Base;
using FeriasTJBase.Infra.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Logging.ClearProviders();
builder.Logging.AddConsole(); // Log no console
builder.Logging.AddFile("Logs/app-{Date}.log");

builder.Services.AddScoped<IFeriasRepository, FeriasRepository>();
builder.Services.AddScoped<IUsufrutoService, UsufrutoService>();
builder.Services.AddScoped<IUsufrutoRepository, UsufrutoRepository>();
builder.Services.AddSingleton<IDescriptografiaService, DescriptografiaService>();
builder.Services.AddHostedService<RabbitMqEscuta>();
builder.Services.AddScoped<IFeriasService, FeriasSerivce>();


builder.Services.AddDbContext<PgDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PgConnection")));
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
