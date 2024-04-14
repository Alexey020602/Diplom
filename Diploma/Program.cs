using DataBase.Data;
using Diploma.Repositories;
using Diploma.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationContext>(options => options.UseNpgsql(connectionString).LogTo(Console.WriteLine));
builder.Services.AddScoped<IPartnerTypesRepository, PartnerTypeRepository>();
builder.Services.AddScoped<IPartnersRepository, PartnersRepository>();
builder.Services.AddScoped<IDirectionsRepository, DirectionsRepository>();
builder.Services.AddTransient<IDivisionRepository, DivisionsRepository>();
builder.Services.AddTransient<IAgreementRepository, AgreementRepository>();
builder.Services.AddTransient<IFacultyRepository, FacultyRepository>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(o =>
    {
    });


builder.Services.AddCors();
var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
    await context.Database.MigrateAsync();
}
    app.UseStaticFiles();
app.UseBlazorFrameworkFiles();
app.UseCors(builder => builder.AllowAnyOrigin()
.AllowAnyHeader()
.AllowAnyMethod()
 ) ;

if (app.Environment.IsDevelopment())
{
    //app.UseWebAssemblyDebugging();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.MapFallbackToFile("index.html");
app.Run();