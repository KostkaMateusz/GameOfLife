
using FluentValidation;
using GameOfLifeApi.GameEngine;
using GameOfLifeApi.Models;

namespace GameOfLifeApi.RequestHandlers;
public static class ApiEngine
{
    public static IResult GetData(DataTable table, IValidator<DataTable> validator)
    {
        var validationResult = validator.Validate(table);

        if (!validationResult.IsValid)
        {
            return Results.BadRequest(validationResult.Errors);
        }
        var engine = new GameOfLifeEngine(table.Data.ToArray());
        var data = engine.CalculateNext();
        var dataConvert = GameOfLifeEngine.ConvertToArray(data);

        return Results.Ok(dataConvert);
    }
}
