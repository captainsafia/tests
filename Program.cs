using System.Text.Json;
using System.Text.Json.Schema;
using System.Text.Json.Serialization;

var extendsDictionarySchema = JsonSchemaExporter.GetJsonSchemaAsNode(JsonSerializerOptions.Default.GetTypeInfo(typeof(ExtendsDictionary)));
Console.WriteLine(extendsDictionarySchema.ToJsonString());

var typeWithExtensionDataSchema = JsonSchemaExporter.GetJsonSchemaAsNode(JsonSerializerOptions.Default.GetTypeInfo(typeof(TypeWithExtensionData)));
Console.WriteLine(typeWithExtensionDataSchema.ToJsonString());

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