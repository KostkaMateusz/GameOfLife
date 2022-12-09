using FluentValidation;
using GameOfLifeApi.Models;
using GameOfLifeApi.RequestHandlers;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddValidatorsFromAssemblyContaining(typeof(ModelValidator));

//Cors Settings
builder.Services.AddCors(options =>
{
    options.AddPolicy("Allow all origins", policyBuilder =>

        policyBuilder.AllowAnyMethod()
            .AllowAnyHeader()
            .WithOrigins(builder.Configuration["AllowedOrigins"])

        );
});


var app = builder.Build();
app.MapPost("/", ApiEngine.GetData).Produces<double[][]>(StatusCodes.Status200OK).Accepts<DataTable>("application/json").WithTags("Game of Life API");
// Configure the HTTP request pipeline.
app.UseCors("Allow all origins");
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();


app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
           Path.Combine(builder.Environment.ContentRootPath, "webroot")),
    RequestPath = "/gameOfLife"
});

app.Run();

