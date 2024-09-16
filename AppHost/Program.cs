using Microsoft.Extensions.Hosting;
var builder = DistributedApplication.CreateBuilder(args);
var username = builder.AddParameter("username", true);
var password = builder.AddParameter("password", true);
var postgres = builder
    .AddPostgres("postgres", password: password)
    // .PublishAsConnectionString()
    .WithPgAdmin()
    .AddDatabase("DiplomaDb");
builder.AddProject<Projects.Diploma>("diploma")
    .WithReference(postgres);
builder.Build().Run();