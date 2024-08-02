using System.Reflection;
using System.Xml.Linq;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Http.Metadata;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapPost("/weatherforecast", (WeatherForecast? forecast) =>
{
    return Results.Ok(forecast);
});

app.Run();

public class WeatherForecast : IBindableFromHttpContext<WeatherForecast>, IEndpointParameterMetadataProvider
{
    public string Date { get; set; }
    public int TemperatureC { get; set; }
    public string Summary { get; set; }

    public static async ValueTask<WeatherForecast?> BindAsync(HttpContext context, ParameterInfo parameter)
    {
        // If this request has an XML content type, we'll deserialize the body into a WeatherForecast
        if (context.Request.ContentType == "application/xml")
        {
            var xmlDoc = await XDocument.LoadAsync(context.Request.Body, LoadOptions.None, CancellationToken.None);
            var serializer = new XmlSerializer(typeof(WeatherForecast));
            return (WeatherForecast?)serializer.Deserialize(xmlDoc.CreateReader());
        }

        return null;
    }

    public static void PopulateMetadata(ParameterInfo parameter, EndpointBuilder builder)
    {
        var metadata = new AcceptsMetadata(["application/xml", "text/xml"], typeof(WeatherForecast), isOptional: true);
        builder.Metadata.Add(metadata);
    }
}