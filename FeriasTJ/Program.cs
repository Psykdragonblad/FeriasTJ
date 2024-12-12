using FeriasTJ.Application.Interface;
using FeriasTJ.Application.Service;
using FeriasTJ.Infra.Interface;
using FeriasTJ.Infra.Messaging;
using FeriasTJ.Infra.Security;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

var b = builder.Environment.WebRootPath;
builder.Services.AddScoped<IExcelService>(provider =>
{
    //var rabbitMqEnvia = provider.GetRequiredService<IRabbitMqEnvia>();
    var filePath = Directory.GetCurrentDirectory(); // ou outro caminho
    //var logger = provider.GetRequiredService<ILogger<ExcelService>>();
    return new ExcelService(filePath);
});
builder.Services.AddScoped<IProcessarArquivoService, ProcessarArquivoService>();

builder.Services.AddControllers();

builder.Logging.ClearProviders();
builder.Logging.AddConsole(); // Log no console
builder.Logging.AddFile("Logs/app-{Date}.log");

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
