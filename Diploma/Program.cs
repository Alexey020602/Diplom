using DataBase.Data;
using Diploma;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
//string connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationContext>();
builder.Services.AddScoped<IPartnersDataManager, PartnersDataManager>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(o =>
    {
    });

//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7217/") });

builder.Services.AddCors();

var app = builder.Build();

app.UseBlazorFrameworkFiles();

app.UseCors(builder => builder.AllowAnyOrigin()
.AllowAnyHeader()
.AllowAnyMethod()
 ) ;

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.MapControllerRoute(
    name: "lists",
    pattern: "{controller=Partners}/{action}"
    );

app.MapFallbackToFile("index.html");
app.Run();