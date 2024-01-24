using Newtonsoft.Json.Converters;
using Taxes.Business;
using Taxes.Database;
using Taxes.Infrastructure.Authorization;
using Taxes.Infrastructure.ExceptionHandling;
using Taxes.Infrastructure.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
ConfigurationManager configuration = builder.Configuration;
builder.Services.AddMssqlDatabase(configuration.GetConnectionStringsModel().MSSQL);
builder.Services.RegisterBusinesServices();

builder.Services.AddControllers().AddNewtonsoftJson(options=>

    options.SerializerSettings.Converters.Add(new StringEnumConverter()));

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

app.UseHttpsRedirection();

app.AddCustomExceptionHandling();
app.UseCustomAuthorization();

app.MapControllers();

app.Run();
