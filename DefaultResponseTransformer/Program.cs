using System.ComponentModel;
using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddOpenApi(options =>
{
    options.AddDefaultProblemDetailsResponse();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", app.Environment.ApplicationName);
    });
}

app.UseHttpsRedirection();

app.MapGet("/api/ping", () =>
{
    return TypedResults.Ok();
});

app.MapGet("/api/pong", () =>
{
    return TypedResults.Ok();
});

app.Run();

internal static class OpenApiOptionsExtensions
{
    public static OpenApiOptions AddDefaultProblemDetailsResponse(this OpenApiOptions options)
    {
        options.AddDocumentTransformer<DefaultResponseDocumentTransformer>();
        options.AddOperationTransformer<DefaultResponseOperationTransformer>();
        return options;
    }
}

internal class DefaultResponseDocumentTransformer : IOpenApiDocumentTransformer
{
    public Task TransformAsync(OpenApiDocument document, OpenApiDocumentTransformerContext context, CancellationToken cancellationToken)
    {
        document.Components ??= new OpenApiComponents();
        document.Components.Schemas.TryAdd(nameof(ProblemDetails), new OpenApiSchema
        {
            Type = "object",
            Properties = new Dictionary<string, OpenApiSchema>
            {
                ["type"] = new OpenApiSchema
                {
                    Type = "string"
                },
                ["title"] = new OpenApiSchema
                {
                    Type = "string"
                },
                ["status"] = new OpenApiSchema
                {
                    Type = "integer"
                },
                ["detail"] = new OpenApiSchema
                {
                    Type = "string"
                }
            }
        });

        return Task.CompletedTask;
    }
}

internal class DefaultResponseOperationTransformer : IOpenApiOperationTransformer
{
    private static readonly OpenApiResponse defaultResponse = new()
    {
        Description = "Error",
        Content = new Dictionary<string, OpenApiMediaType>
        {
            [MediaTypeNames.Application.ProblemJson] = new()
            {
                Schema = new OpenApiSchema()
                {
                    Reference = new OpenApiReference()
                    {
                        Type = ReferenceType.Schema,
                        Id = nameof(ProblemDetails)
                    }
                }
            }
        }
    };

    public Task TransformAsync(OpenApiOperation operation, OpenApiOperationTransformerContext context, CancellationToken cancellationToken)
    {
        operation.Responses.TryAdd("default", defaultResponse);
        return Task.CompletedTask;
    }
}