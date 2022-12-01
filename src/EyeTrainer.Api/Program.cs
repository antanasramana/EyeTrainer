using EyeTrainer.Api.Bootstrap;
using Microsoft.EntityFrameworkCore;
using EyeTrainer.Api.Data;
using Hellang.Middleware.ProblemDetails;
using EyeTrainer.Api.Constants;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<EyeTrainerApiContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("EyeTrainerApiContext") ?? throw new InvalidOperationException("Connection string 'EyeTrainerApiContext' not found.")));

IConfiguration configuration = builder.Configuration;
// Add services to the container.
builder.Services.SetupAutoMapper();
builder.Services.SetupDependencies();
builder.Services.SetupProblemDetails();
builder.Services.SetupAuthorization(configuration);

builder.Services.AddControllers()
    .AddNewtonsoftJson();

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

app.UseCors(Policy.DevelopmentCors);
app.CreateInitialData();
app.UseHttpsRedirection();
app.UseProblemDetails();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
