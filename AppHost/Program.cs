using Microsoft.Extensions.Hosting;
var builder = DistributedApplication.CreateBuilder(args);
var username = builder.AddParameter("username", true);
var password = builder.AddParameter("password", true);
var postgres = builder
    .AddPostgres("postgres", password: password)
    // .PublishAsConnectionString()
    .WithPgAdmin()
    .AddDatabase("DiplomaDb");
var migrations = builder.AddProject<Projects.MigrationService>("migrations")
    .WithReference(postgres);

builder.AddProject<Projects.Diploma>("diploma")
    .WithReference(migrations)
    .WithReference(postgres);

var app = builder.Build();
await app.RunAsync();