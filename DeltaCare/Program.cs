using DeltaCare.BAL;
using DeltaCare.Common;
using DeltaCare.Configuration;
using DeltaCare.DAL;
using DeltaCare.Extension;
using DeltaCare.Middleware;
using QuestPDF.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Retrieve AppSettings configuration
var configurationAppsetting = builder.Configuration.Get<AppSettings>();

// Configure email settings from configuration
builder.Services.Configure<EmailConfiguration>(builder.Configuration.GetSection("EmailConfiguration"));

// Register services and configure DI container
builder.Services
    .AddPresentation()
    .AddCors(builder.Configuration)  // Configure CORS based on the settings in appsettings.json
    .AddAuthenticationAndAuthorization(configurationAppsetting)  // Setup JWT authentication and authorization
    .AddDbConnectionString(builder.Configuration)  // Add database connection string from configuration  
    .RegisterDALServices() // Register data access layer services
    .AddScoped<ExceptionHandlingMiddleware>()  // Register exception handling middleware
    .RegisterBALServices(configurationAppsetting); // Register business layer services


// Initialize the app instance
var app = builder.Build();
// Configure helper classes and initialize dependencies
ConfigHelper.Initialize(app.Services);

// Set QuestPDF to Community license
app.ConfigureThirdPartyLicenses();

// Configure the HTTP request pipeline
await app.ConfigurePipeline(configurationAppsetting);
await app.RunAsync();


