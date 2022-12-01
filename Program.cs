using GameOfLife;
using System.Diagnostics;


// To Remove After Testing
var stopwatch = new Stopwatch();
stopwatch.Start();

// instance of engine
var game = new GameOfLifeEngine(10, 10);

// external reference for object dable 
// for debuging
Cell[,] table = game.table;


// TODO add method to add new points in run
table[3, 4].currentValue = true;
table[3, 5].currentValue = true;
table[3, 6].currentValue = true;

// Generate runs
for (int k = 0; k < 51; k++)
{
    game.CalculateNext();
}

//display 
// TODO implement to string method for debubing
for (int i = 0; i < table.GetLength(0); i++)
{
    for (int j = 0; j < table.GetLength(1); j++)
    {
        Console.Write(table[i, j].currentValue.ToString() + " ");
    }
    Console.WriteLine();
}
Console.WriteLine();



stopwatch.Stop();
Console.WriteLine(stopwatch.ElapsedMilliseconds);


