using MyAppKafka.Application;
using MyAppKafka.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

//builder.Services.Configure<KafkaSettings>(builder.Configuration.GetSection("Kafka"));

// Application services
builder.Services.AddScoped<IOrderService, OrderService>();

// Infrastructure
builder.Services.AddSingleton<IMessageProducer, KafkaProducer>();
builder.Services.AddHostedService<KafkaConsumerHostedService>();

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapControllers();
app.Run();