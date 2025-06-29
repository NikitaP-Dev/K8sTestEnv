using Prometheus;
using Sample.Web;
using test;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Define an HTTP client that reports metrics about its usage, to be used by a sample background service.
builder.Services.AddHttpClient(SampleService.HttpClientName);

// Export metrics from all HTTP clients registered in services
builder.Services.UseHttpClientMetrics();

// A sample service that uses the above HTTP client.
builder.Services.AddHostedService<SampleService>();

builder.Services.AddHealthChecks()
    // Define a sample health check that always signals healthy state.
    .AddCheck<SampleHealthCheck>(nameof(SampleHealthCheck))
    // Report health check results in the metrics output.
    .ForwardToPrometheus();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseHttpMetrics();

app.UseAuthorization();

app.MapMetrics();

app.MapStaticAssets();

app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
