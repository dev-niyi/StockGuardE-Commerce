using System.Text.Json.Serialization;
using Serilog;
using StockGuard.Application;
using StockGuard.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container

builder.Services.AddMediatR(cfg =>
{
	cfg.RegisterServicesFromAssembly
			(StockGuard.Application.AssemblyReference.Assembly);
});

builder.Services.AddControllers().AddJsonOptions(options
	=> options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

builder.Services
	.AddApplication(builder.Configuration)
	.AddInfrastructure(builder.Configuration);

builder.Host.UseSerilog((context, configuration)
	=> configuration.ReadFrom.Configuration(context.Configuration));

builder.Services.AddEndpointsApiExplorer();

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
