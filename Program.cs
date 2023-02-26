using Microsoft.EntityFrameworkCore;

using FastDeliveryApi.Data;
using FastDeliveryApi.Middleware;
using FastDeliveryApi.Repositories;
using FastDeliveryApi.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.Decorate<ICustomerRepository, CachedCustomerRepository>();

builder.Services.AddMemoryCache();

var connectionString = builder.Configuration.GetConnectionString("MyDbPgsql");
builder.Services.AddDbContext<FastDeliveryDbContext>(options =>
{
    options.UseNpgsql(connectionString);
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionMiddleware>();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();