using Microsoft.EntityFrameworkCore;
using Serilog;
using Subscriber.Data;
using Subscriber.WebApi.Config;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;
builder.Services.AddControllers();
builder.Services.ConfigureServices();

object value = builder.Host.UseSerilog((context, configuration) =>
{
    configuration.ReadFrom.Configuration(context.Configuration);

});
builder.Services.AddDbContext<WeightWatcherContext>(option =>
{
    option.UseSqlServer(configuration.GetConnectionString("WeightWatchersConnectionString"));
}
);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseSerilogRequestLogging();
app.UseCors(builder =>
{
    builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader();
});
app.UseAuthorization();

app.MapControllers();
app.UseMiddleware(typeof(Middleware));

app.Run();
