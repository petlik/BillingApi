using Billing.Api.Services;
using Billing.Api.Configs;
using Billing.Clients.Example;
using Billing.Clients.Thrower;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(x =>
{
    // serialize enums as strings in api responses (e.g. Role)
    x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddTransient<IExamplePaymentConfig, ExamplePaymentConfig>();
builder.Services.AddTransient<ExamplePaymentClient>();

builder.Services.AddTransient<ThrowerPaymentClient>();

builder.Services.AddTransient<IOrderService, OrderService>(); 
builder.Services.AddTransient<IPaymentGatewaySelector, PaymentGatewaySelector>();
builder.Services.AddTransient<IRecieptService, RecieptService>();

builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
