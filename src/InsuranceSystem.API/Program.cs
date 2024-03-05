using InsuranceSystem.API.Extensions;
using Microsoft.AspNetCore.Hosting;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureCors();
builder.Services.ConfigureRepositoryManager();
builder.Services.ConfigureServiceManager();
builder.Services.ConfigureFilters();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.ConfigureSqlContext(builder.Configuration);

#region Logging Services
string[] systemDrives = Environment.GetLogicalDrives();
string logLocationDrive = null;
if (systemDrives.Length >= 0)
{
    logLocationDrive = systemDrives[0];
}
var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
Log.Logger = new LoggerConfiguration()
           .MinimumLevel.Override("Microsoft.EntityFrameworkCore.Database.Command", Serilog.Events.LogEventLevel.Warning)
                .WriteTo.File($"C:\\InsuranceLogs\\api_logs\\api-{DateTime.Now.ToString("dd-MM-yyyy")}.txt")
                .ReadFrom.Configuration(configuration).CreateLogger();
builder.Services.AddSingleton(Log.Logger);
builder.Services.ConfigureLogger();

System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("CorsPolicy");

app.UseAuthorization();
app.Use(async (context, next) =>
{
    context.Response.Headers.Add("X-Frame-Options", "DENY");
    context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
    context.Response.Headers.Add("Content-Security-Policy", "frame-ancestors");
    context.Response.Headers.Add("Strict-Transport-Security", "max-age=31536000; includeSubDomains; preload");
    context.Response.Headers.Add("X-XSS-Protection", "1;mode=block");
    await next();
});

app.MapControllers();
try
{
    Log.Information("Application Starting...");
    app.Run();
    Log.Information("Application Started!");
}
catch (Exception e)
{
    Log.Error(e, "Application stopped because of exception");
    throw;
}
finally
{
    Log.CloseAndFlush();
}
