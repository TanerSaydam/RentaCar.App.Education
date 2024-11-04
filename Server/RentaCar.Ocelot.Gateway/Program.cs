using MMLib.Ocelot.Provider.AppConfiguration;
using MMLib.SwaggerForOcelot.DependencyInjection;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddOcelotWithSwaggerSupport(options =>
{
    options.Folder = "Configuration";
});

builder.Services.AddOcelot(builder.Configuration)
    .AddAppConfiguration();

builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerForOcelot(builder.Configuration);

builder.Services.AddCors();

builder.Services.AddOpenTelemetry()
    .WithTracing(configure =>
    {
        configure
        .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("Ocelot.Gateway"))
        .AddAspNetCoreInstrumentation()
        .AddHttpClientInstrumentation()
        .AddConsoleExporter()
        .AddOtlpExporter(options =>
        {
            string jeagerEndpoint = builder.Configuration.GetSection("Endpoints")["Jeager"]!.ToString();
            options.Endpoint = new Uri(jeagerEndpoint);
        });
    });

var app = builder.Build();

app.UseCors(x => x.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());

app.UseSwaggerForOcelotUI(opt =>
{
    opt.PathToSwaggerGenerator = "/swagger/docs";
}).UseOcelot().Wait();

app.Run();
