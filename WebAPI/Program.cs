using AutoMapper.Extensions.ExpressionMapping;
using WebAPI.Application.Profiles;
using WebAPI.Application.UseCase;
using WebAPI.Application.UseCase.Interfaces;
using WebAPI.Domain.IRepositories;
using WebAPI.Domain.Services;
using WebAPI.Domain.Services.IService;
using WebAPI.Infrastructure.Contexts;
using WebAPI.Infrastructure.Profiles;
using WebAPI.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
Console.WriteLine($"Application Name: {builder.Environment.ApplicationName}");
Console.WriteLine($"Environment Name: {builder.Environment.EnvironmentName}");

builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<InfrastructureProfile>();
    cfg.AddProfile<ApplicationProfile>();
    cfg.AddExpressionMapping();
});

//Dependency Injection
builder.Services.AddScoped<IConsultaUseCase, ConsultaUseCase>();
builder.Services.AddScoped<IConsultaService, ConsultaService>();
builder.Services.AddScoped<IConsultaRepository, ConsultaRepository>();

builder.Services.AddScoped<WebAPIContext>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "MicroServicio KOHA",
        Version = "v1"
    });
});

var app = builder.Build();

app.UseAuthentication(); 

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

bool IsDevelopmentEnvironment(string env) => env == "Development";

bool IsStagingEnvironment(string env) => env == "Staging";

bool IsProductionEnvironment(string env) => env == "Production";

