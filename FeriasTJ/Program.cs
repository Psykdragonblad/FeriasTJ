using FeriasTJ.Application.Interface;
using FeriasTJ.Infra.Interface;
using FeriasTJ.Infra.Messaging;
using FeriasTJ.Infra.Security;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<IRabbitMqEnvia, RabbitMqEnvia>();
builder.Services.AddScoped<ICriptografiaSerivce, CriptografiaService>();

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
