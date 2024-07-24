var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/non-nullable-array", (int[] ints, HttpContext context) =>
{
    return ints;
});

app.MapGet("/nullable-array", (int?[] ints, HttpContext context) =>
{
    return ints;
});

app.MapGet("/non-nullable", (int ints, HttpContext context) =>
{
    return ints;
});

app.MapGet("/nullable", (int? ints, HttpContext context) =>
{
    return ints;
});

app.Run();
