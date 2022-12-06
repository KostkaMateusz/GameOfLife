using GameOfLife;



int mapXSize = 100;
int mapYSize = 100;
int numbersOfRuns = 50;


int xMapResolution = 2400;
int yMapResolution = 2400;

/*ToDo:
 * Add Grid to HeatMap
 * Configure Labels in HeatMap
 * COnfigure Axes in HeatMap
 * Refactor Drawing Heat Map to new Clas
 */

var plt = new ScottPlot.Plot(xMapResolution, yMapResolution);
var gameOfLifeEngine = new GameOfLifeEngine(mapXSize, mapYSize);

gameOfLifeEngine.InsertIntoGame(55, 44, true);
gameOfLifeEngine.InsertIntoGame(55, 45, true);
gameOfLifeEngine.InsertIntoGame(55, 46, true);

gameOfLifeEngine.InsertIntoGame(53, 45, true);
gameOfLifeEngine.InsertIntoGame(56, 45, true);


// Generate runs
for (int k = 0; k < numbersOfRuns; k++)
{
    var nextStep=gameOfLifeEngine.CalculateNext();
    var data2D = GameOfLifeEngine.ConvertToArray(nextStep);    

    plt.AddHeatmap(data2D);
    plt.SaveFig($"heatmap_quickstart{k}.png");
    
}

