using System.Reflection;
using Microsoft.OpenApi.Models;
using UnivAssurance.WebAPI.Logging;
using NLog;
using UnivAssurance.DataAccess.Data;
using Microsoft.AspNetCore.Cors;
using Microsoft.EntityFrameworkCore;
using UnivAssurance.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

// Charge l'environement de l'application
var builder = WebApplication.CreateBuilder(args);

LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/bin/Debug/net7.0/NLog.config"));

// Charge les controllers de la'application
builder.Services.AddControllers();

string? corsOrigin = builder.Configuration.GetSection("CorsOrigin").Get<string>();

string[] corsTable = corsOrigin.Split(new char[','], StringSplitOptions.RemoveEmptyEntries);

// Charge les services de swagger
builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc("v1", new OpenApiInfo {
        Title = "Gestion de souscription",
        Description = "Api de souscription d'assurance"
    });

    string NomFichier = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    string SwaggerXML = Path.Combine(AppContext.BaseDirectory, NomFichier);

    c.IncludeXmlComments(SwaggerXML);
});

// Db context

builder.Services.AddDbContext<UnivAssuranceDBContext>(option => {
    string? dbConnection = builder.Configuration.GetConnectionString("AbonnementCs");
    option.UseSqlServer(dbConnection);
});

builder.Services.AddDbContext<AuthDbContext>(option => {
    string? dbConnection = builder.Configuration.GetConnectionString("AbonnementCs");
});

builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<AuthDbContext>().AddDefaultTokenProviders();

builder.Services.AddAuthentication(auth => auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(
    options =>
    {
        options.SaveToken = true;
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidAudience = "http://localhost:5161" ,
            ValidIssuer = "http://localhost:5161" ,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ByYM000OLlMQG6VVVp1OH7Xzyr7gHuw1qvUC5dcGt3SNM"))
        };
    }
);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSingleton<ILog, Log>();
builder.Services.AddCors(
    o => {
        o.AddPolicy(
            "SouscriptionCors",
            p => p.WithOrigins(corsTable)
            .WithHeaders()
            .WithMethods()
        );
    }
);

// App va permetre d'intéragir avec l'appication
// ex: les routes, etc...
var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

// Permet d'utiliser les routes
app.MapControllers();

// Permet d'utiliser l'interface de swagger
app.UseSwagger();
app.UseSwaggerUI(c => { 
    c.RoutePrefix = string.Empty;
    c.SwaggerEndpoint("/swagger/v1/swagger.js", "v1"); // Personnalise la route
});

app.UseCors("SouscriptionCors");


app.Run();

var webHost = new WebHostBuilder().UseIISIntegration().Build();
