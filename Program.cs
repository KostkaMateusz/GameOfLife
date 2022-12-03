using GameOfLife;
using System.Diagnostics;


// To Remove After Testing
var stopwatch = new Stopwatch();
stopwatch.Start();

var mapXSize = 100;
var mapYSize = 100;
var xMapResolution = 2400;
var yMapResolution = 2400;

var plt = new ScottPlot.Plot(xMapResolution, yMapResolution);
var gameOfLifeEngine = new GameOfLifeEngine(mapXSize, mapYSize);

gameOfLifeEngine.InsertIntoGame(55, 44, true);
gameOfLifeEngine.InsertIntoGame(55, 45, true);
gameOfLifeEngine.InsertIntoGame(55, 46, true);

gameOfLifeEngine.InsertIntoGame(53, 45, true);
gameOfLifeEngine.InsertIntoGame(56, 45, true);



// Generate runs
for (int k = 0; k < 50; k++)
{
    var nextStep=gameOfLifeEngine.CalculateNext();
    var data2D = GameOfLifeEngine.ConvertToArray(nextStep);    

    plt.AddHeatmap(data2D);
    plt.SaveFig($"heatmap_quickstart{k+1}.png");
    
}


stopwatch.Stop();
Console.WriteLine(stopwatch.ElapsedMilliseconds);


