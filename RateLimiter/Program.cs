using RateLimiter;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.ConfigureSwaggerGen(setup =>
{
    setup.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Rate Limiter",
        Version = "v1"
    });
});

var app = builder.Build();

// Azure API Management needs the Swagger definitions to always be present, regardless of the application's environment
app.UseSwagger();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

var bucketRefiller = new BucketRefiller();

var thread1 = new Thread(() => app.Run());
var thread2 = new Thread(() => bucketRefiller.Refill(RateLimiterSingleton.Instance));

thread1.Start();
thread2.Start();


// Only once instance of Rate Limiter is running
// Not thread-safe / Concurrency issue
// - Refiller didn't refill although should have => will refill next cycle
// - Limiter rejected a call although should not have => well, try again

// Where to add lock?
// display when to try again
// publish to Azure
// check if publishing to Azure is okay to have a public Github repo