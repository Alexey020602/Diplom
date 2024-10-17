using DataBase.Data;
using Microsoft.EntityFrameworkCore;
using MigrationService;
Console.WriteLine("Begin");
var builder = Host.CreateApplicationBuilder(args);
Console.WriteLine("Create builder");
builder.Services.AddTransient<ApplicationContextSeed>();
builder.AddServiceDefaults();
builder.Services.AddOpenTelemetry()
    // .WithTracing(tracing => tracing.AddSource(Worker.ActivitySourceName))
    ;
builder.Services.AddLogging();
//todo Добавить какой-то флаг для запуска, чтобы было понятно, что надо брать контекст из Aspire
builder.AddNpgsqlDbContext<ApplicationContext>("DiplomaDB");
// builder.Services.AddDbContext<ApplicationContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddHostedService<Worker>();
Console.WriteLine("Create Hosted Services");
var host = builder.Build();
Console.WriteLine("Created host");
host.Run();
