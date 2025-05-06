var builder = DistributedApplication.CreateBuilder(args);
var postgres = builder
    .AddPostgres("postgres")
    .WithDataVolume()
    .WithPgAdmin()
    .AddDatabase("DiplomaDb");

var migrations = builder.AddProject<Projects.MigrationService>("migrations")
    .WithReference(postgres)
    .WaitFor(postgres);

builder.AddProject<Projects.Diploma>("diploma")
    .WaitFor(migrations)
    .WithReference(postgres);

var app = builder.Build();
app.Run();