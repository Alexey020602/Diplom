using System.Text;
using DataBase.Data;
using DataBase.Models.Identity;
using Diploma.Repositories;
using Diploma.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

var validIssuer = builder.Configuration.GetValue<string>("JwtTokenSettings:ValidIssuer");
var validAudience = builder.Configuration.GetValue<string>("JwtTokenSettings:ValidAudience");
var symmetricSecurityKey = builder.Configuration.GetValue<string>("JwtTokenSettings:SymmetricSecurityKey");

builder.Services.AddControllers().AddNewtonsoftJson();

builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    //.AddRoleManager<RoleStore<IdentityRole>>()
    .AddEntityFrameworkStores<ApplicationContext>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, o =>//Íàñòðîéêè ïðîâåðêè òîêåíà
    {
        o.RequireHttpsMetadata = false;
        o.IncludeErrorDetails = true;
        o.SaveToken = true;
        o.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = validIssuer,
            ValidateAudience = true,
            ValidAudience = validAudience,
            ValidateIssuerSigningKey = true,
            ValidateLifetime = true,
            RequireExpirationTime = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(symmetricSecurityKey)),
        };
    });
builder.Services.AddAuthorization();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
                       throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationContext>(options =>
    options.UseNpgsql(connectionString).LogTo(Console.WriteLine));

builder.Services.AddTransient<IPartnerTypesRepository, PartnerTypeRepository>();
builder.Services.AddTransient<IPartnersRepository, PartnersRepository>();
builder.Services.AddTransient<IDirectionsRepository, DirectionsRepository>();
builder.Services.AddTransient<IDivisionRepository, DivisionsRepository>();
builder.Services.AddTransient<IAgreementRepository, AgreementRepository>();
builder.Services.AddTransient<IFacultyRepository, FacultyRepository>();
builder.Services.AddTransient<IInteractionRepository, InteractionRepository>();
builder.Services.AddTransient<IInteractionTypeRepository, InteractionTypeRepository>();
builder.Services.AddTransient<IAgreementStatusRepository, AgreementStatusRepository>();
builder.Services.AddTransient<IAgreementTypeRepository, AgreementTypeRepository>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(o =>
{
    var securityScheme = new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme.",
        Name = "Authorization",
        Scheme = "bearer",
        Type = SecuritySchemeType.OAuth2,
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Flows = new()
        {
            Password = new()
            {
                TokenUrl = Uri.TryCreate("/token", UriKind.Relative, out var u) ? u : default,
                Scopes = { { "Admin", "Administrator" }, { "ЦТТ", "Ctt" }, {"ЦИП","Cip"}, },
            },
        },
    };
    o.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, securityScheme);
    securityScheme.Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = JwtBearerDefaults.AuthenticationScheme };
    o.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { securityScheme, new List<string>() }
    });
});


builder.Services.AddCors();


var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
    new ApplicationContextSeed(context, app.Logger, app.Environment.IsDevelopment()).Seed();
}

app.UseStaticFiles();
app.UseBlazorFrameworkFiles();
app.UseCors(policyBuilder => policyBuilder.AllowAnyOrigin()
    .AllowAnyHeader()
    .AllowAnyMethod()
);

if (app.Environment.IsDevelopment())
{
    //app.UseWebAssemblyDebugging();
    app.UseSwagger();
    app.UseSwaggerUI(o=>{
        o.EnablePersistAuthorization();
        o.OAuthScopeSeparator(" ");
        o.OAuthClientId(validAudience);
        o.OAuthClientSecret(symmetricSecurityKey);
        o.OAuthScopes("Admin");
        o.OAuthAppName("SwaggerIU");
    });
}

app.UseAuthentication();
app.UseAuthorization();
app.MapGroup("/api")
    .MapControllers()
    // .RequireAuthorization()
    ;
// app.MapControllers();
app.MapFallbackToFile("index.html");
app.Run();