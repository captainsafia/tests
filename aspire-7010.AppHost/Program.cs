var builder = DistributedApplication.CreateBuilder(args);

var apiService = builder.AddProject<Projects.aspire_7010_ApiService>("apiservice");

var functions = builder.AddAzureFunctionsProject<Projects.aspire_7010_Functions>("functions");

builder.AddProject<Projects.aspire_7010_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(apiService)
    .WaitFor(apiService);

builder.Build().Run();
