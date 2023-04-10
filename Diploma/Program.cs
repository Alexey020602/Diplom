using DataBase.Data;
using Diploma;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
//string connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationContext>();
builder.Services.AddScoped<IPartnersDataManager, PartnersDataManager>();

var app = builder.Build();

app.MapControllerRoute(
    name: "lists",
    pattern: "{controller=Partners}/{action=Index}"
    );

app.Run();