using DataBase.Data;
using MigrationService;
using Npgsql.EntityFrameworkCore.PostgreSQL;

var builder = Host.CreateApplicationBuilder(args);
builder.AddServiceDefaults();
builder.Services.AddOpenTelemetry()
    // .WithTracing(tracing => tracing.AddSource(Worker.ActivitySourceName))
    ;
builder.AddNpgsqlDbContext<ApplicationContext>("DiplomaDB");
builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
