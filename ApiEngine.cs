
using LifeAPI;

namespace GameOfLifeApi;
public static class ApiEngine
{
    public static IResult GetData(DataTable table)
    {
        var engine = new GameOfLifeEngine(table.data.ToArray());
        var data = engine.CalculateNext();
        var dataConvert=GameOfLifeEngine.ConvertToArray(data);

        return Results.Ok(dataConvert);
    }
}
