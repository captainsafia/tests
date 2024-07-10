using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapPost("/extends-dictionary", (ExtendsDictionary dict) => Results.Ok(dict.Name + dict.IsEnabled));
app.MapPost("/type-with-extension-data", (TypeWithExtensionData dict) => Results.Ok(dict.Name + dict.IsEnabled + dict.AdditionalData?.Count));

app.Run();

class ExtendsDictionary : Dictionary<string, Guid>
{
    public required string Name { get; set; }
    public required bool IsEnabled { get; set; }
}

class TypeWithExtensionData
{
    public required string Name { get; set; }
    public required bool IsEnabled { get; set; }

    [JsonExtensionData]
    public Dictionary<string, object>? AdditionalData { get; set; }

}