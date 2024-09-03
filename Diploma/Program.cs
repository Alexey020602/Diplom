using System.Text;
using DataBase.Data;
using DataBase.Models.Identity;
using Diploma;
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

builder.Services.AddIdentity<IdentityUser<Guid>, IdentityRole<Guid>>(options =>
    {
        options.Password.RequireDigit = false;
        options.Password.RequiredLength = 6;
        options.Password.RequireLowercase = false;
        options.Password.RequireUppercase = false;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequiredUniqueChars = 2;
    })
    .AddRoles<IdentityRole<Guid>>()
    .AddEntityFrameworkStores<ApplicationContext>();

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })
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
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("WatchOnly", policy => policy.RequireRole(Role.Cip.ToString()));
    options.AddPolicy("Edit", policy => policy.RequireRole(Role.Cip.ToString(), Role.Ctt.ToString()));
    options.AddPolicy("Admin", policy => policy.RequireRole(Role.Cip.ToString(), Role.Ctt.ToString(), Role.Admin.ToString()));
});

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
builder.Services.AddTransient<ITokenService, TokenService>();

builder.Services.AddLogging();

builder.Services.AddTransient<ApplicationContextSeed>();
builder.Services.AddTransient<IdentitySeed>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(o =>
{
    // var loginSecurityScheme = new OpenApiSecurityScheme
    // {
    //     Description = "JWT Authorization header using the Bearer scheme.",
    //     Name = "Authorization",
    //     Scheme = "bearer",
    //     Type = SecuritySchemeType.OAuth2,
    //     BearerFormat = "JWT",
    //     In = ParameterLocation.Header,
    //     Flows = new()
    //     {
    //         Password = new()
    //         {
    //             TokenUrl = Uri.TryCreate("/login", UriKind.Relative, out var u) ? u : default,
    //             Scopes = { { "Admin", "Administrator" }, { "ЦТТ", "Ctt" }, {"ЦИП","Cip"}, },
    //         },
    //     },
    // };
    var loginSecurityScheme = new OpenApiSecurityScheme
    {
        Description = "JWT Token",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = JwtBearerDefaults.AuthenticationScheme
    };
    o.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, loginSecurityScheme);
    // o.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, tokenSecurityScheme);
    loginSecurityScheme.Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = JwtBearerDefaults.AuthenticationScheme };
    o.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { new OpenApiSecurityScheme
        {
            Reference = new()
            {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
            }
        },
            new List<string>() }
    });
    // o.AddSecurityRequirement(new()
    // {
    //     {tokenSecurityScheme, new List<string>()}
    // });
});


builder.Services.AddCors();


var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var contextSeed = scope.ServiceProvider.GetRequiredService<ApplicationContextSeed>();
    contextSeed.Seed();
    var identitySeed = scope.ServiceProvider.GetRequiredService<IdentitySeed>();
    await identitySeed.Seed();
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
        o.OAuthAppName("SwaggerUI");
    });
}

app.UseAuthentication();
app.UseAuthorization();
// app.MapGroup("/api")
//     .MapControllers()
    // .RequireAuthorization()
    // ;
app.MapControllers();
app.MapFallbackToFile("index.html");

// app.MapGet("/", () => Results.Redirect("index.html"));
app.Run();
