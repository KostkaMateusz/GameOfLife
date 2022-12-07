using FluentValidation;
using GameOfLifeApi;
using LifeAPI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddValidatorsFromAssemblyContaining(typeof(ModelValidator));
var app = builder.Build();
app.MapPost("/", ApiEngine.GetData).Produces<double[][]>(StatusCodes.Status200OK).Accepts<DataTable>("application/json").WithTags("Game of Life API");
// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.Run();

