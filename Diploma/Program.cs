using DataBase.Data;
using Diploma.Services;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationContext>();
builder.Services.AddScoped<IPartnerTypesRepository, PartnerTypeRepository>();
builder.Services.AddScoped<IPartnersRepository, PartnersRepository>();
builder.Services.AddScoped<IDirectionsRepository, DirectionsRepository>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(o =>
    {
    });


builder.Services.AddCors();
var app = builder.Build();
app.UseStaticFiles();
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

app.MapControllers();
app.MapFallbackToFile("index.html");
app.Run();