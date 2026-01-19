using Host.Worker;

var builder = Microsoft.Extensions.Hosting.Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
