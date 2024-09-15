var builder = DistributedApplication.CreateBuilder(args);
builder.AddProject<Projects.Diploma>("diploma");
builder.Build().Run();