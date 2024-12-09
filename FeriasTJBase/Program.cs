using FeriasTJBase.Application.Interface;
using FeriasTJBase.Application.Services;
using FeriasTJBase.Domain.Interface;
using FeriasTJBase.Infra.Configurations;
using FeriasTJBase.Infra.Messaging;
using FeriasTJBase.Infra.Repositories;
using FeriasTJBase.Infra.Security;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<IFeriasRepository, FeriasRepository>();
builder.Services.AddSingleton<IDescriptografiaService, DescriptografiaService>();
builder.Services.AddHostedService<RabbitMqEscuta>();
builder.Services.AddScoped<IFeriasService, FeriasSerivce>();


builder.Services.AddDbContext<PgDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PgConnection")));
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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
