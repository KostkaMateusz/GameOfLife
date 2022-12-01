using GameOfLife;
using System.Diagnostics;
using System.Runtime.Serialization.Formatters;
//Any live cell with two or three live neighbours survives.
//Any dead cell with three live neighbours becomes a live cell.
//All other live cells die in the next generation. Similarly, all other dead cells stay dead.

var stopwatch = new Stopwatch();
stopwatch.Start();

var game = new GameOfLifeEngine(6000, 6000);

Cell[,] table = game.table;

table[3, 4].currentValue = true;
table[3, 5].currentValue = true;
table[3, 6].currentValue = true;


for (int k = 0; k < 5; k++)
{
    game.CalculateNext();
    //for (int i = 0; i < table.GetLength(0); i++)
    //{
    //    for (int j = 0; j < table.GetLength(1); j++)
    //    {
    //        Console.Write(table[i, j].currentValue.ToString() + " ");
    //    }
    //    Console.WriteLine();
    //}
    //Console.WriteLine();
}




Console.WriteLine(table);

stopwatch.Stop();
Console.WriteLine(stopwatch.ElapsedMilliseconds);


class GameOfLifeEngine
{
    public Cell[,] table;

    public GameOfLifeEngine(int sizeX, int sizeY)
    {
        table = new Cell[sizeX, sizeY];

        // initalize objects Cells
        int height = table.GetLength(0);
        int width = table.GetLength(1);

        Parallel.For(0, height, y =>
        {
            for (int x = 0; x < width; ++x)
            {
                table[y, x] = new Cell(y, x, table.GetLength(0), table.GetLength(1));

            }
        });
            //    for (int i = 0; i < table.GetLength(0); i++)
            //for (int j = 0; j < table.GetLength(1); j++)
    }

    public Cell[,] CalculateNext()
    {
        int height = table.GetLength(0);
        int width = table.GetLength(1);

        Parallel.For(0, height, y =>
        {
            for (int x = 0; x < width; ++x)
            {               
                int number_of_live_cells = 0;
                foreach (var neighbourAddress in table[y, x].NeighborsAddresses)
                {
                    var neighbour = table[neighbourAddress.xPosition, neighbourAddress.yPosition];
                    if (neighbour != null && neighbour.currentValue == true)
                        number_of_live_cells++;
                }

                if (table[y, x].currentValue == true && (number_of_live_cells == 2 || number_of_live_cells == 3))
                {
                    table[y, x].futureValue = true;
                }
                else if (number_of_live_cells == 3)
                {
                    table[y, x].futureValue = true;
                }
                else
                {
                    table[y, x].futureValue = false;
                }
            }
        });

        PrepareToNextIteration();

        return table;
    }
    private void PrepareToNextIteration()
    {
        int i = table.GetLength(0);
        int j = table.GetLength(1);

        Parallel.For(0, i, y =>
        {
            for (int x = 0; x < j; ++x)
            {
                table[y, x].Prepare();
            }
        });
    }

}
