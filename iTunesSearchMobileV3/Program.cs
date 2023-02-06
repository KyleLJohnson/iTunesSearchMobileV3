using iTunesSearchMobileV3;
using iTunesSearchMobileV3.Data;
using iTunesSearchMobileV3.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using System.Reflection;

#region "Builder Configuration"
var builder = WebApplication.CreateBuilder(args);
string connString = builder.Configuration["CosmosDBConnectionString"] ?? "";

builder.Services.AddTransient<IITunesSearchMobileService, ITunesSearchMobileService>();
builder.Services.AddDbContext<TrackClickCountDbContext>(opt => opt.UseCosmos(connString,
    databaseName: "iTunesSearch")); //.UseInMemoryDatabase("TrackClickCount"));

// The following line enables Application Insights telemetry collection.
builder.Services.AddApplicationInsightsTelemetry();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "iTunes Search API",
        Description = "A .NET 7 Minimal API for searching iTunes",
        TermsOfService = new Uri("https://example.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "Developer Contact",
            Url = new Uri("https://linkedin.com/in/kyleljohnson")
        },
    });

    // using System.Reflection;
    //var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    //options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddCors();

#endregion

#region "App Configuration"

var app = builder.Build();
//using var scope = app.Services.CreateScope();
//var db = scope.ServiceProvider.GetService<TrackClickCountDbContext>();
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("./swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});
//}

app.UseCors(policy =>
    policy.WithOrigins("https://localhost:5115")
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true)); // allow any origin  
   // .WithHeaders(HeaderNames.ContentType));


#endregion
app.MapGroup("/searchresults")
    .MapiTunesSearchMobileApi()
    .WithTags("ISM Endpoints");

app.Run();
