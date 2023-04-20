using DataBase.Data;
using Diploma;
using Microsoft.OpenApi.Models;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
//string connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationContext>();
builder.Services.AddScoped<IPartnersDataManager, PartnersDataManager>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(o =>
    {
    });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllerRoute(
    name: "lists",
    pattern: "{controller=Partners}/{action}"
    );

app.Run();