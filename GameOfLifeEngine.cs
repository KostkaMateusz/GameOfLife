namespace GameOfLife;

class GameOfLifeEngine
{
    private Cell[,] table;

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
                Cell? neighbour;
                foreach (var neighbourAddress in table[y, x].NeighborsAddresses)
                {
                    if (number_of_live_cells > 3) continue;
                    neighbour = table[neighbourAddress.xPosition, neighbourAddress.yPosition];
                    if (neighbour != null && neighbour.currentValue == true)
                        number_of_live_cells++;
                }

                //Any live cell with two or three live neighbours survives.
                if (table[y, x].currentValue == true && number_of_live_cells == 2 )
                {
                    table[y, x].futureValue = true;
                }
                //Any dead cell with three live neighbours becomes a live cell.
                else if (number_of_live_cells == 3)
                {
                    table[y, x].futureValue = true;
                }
                //All other live cells die in the next generation. Similarly, all other dead cells stay dead.
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
        int height = table.GetLength(0);
        int width = table.GetLength(1);

        Parallel.For(0, height, y =>
        {
            for (int x = 0; x < width; ++x)
            {
                table[y, x].Prepare();
            }
        });
    }

    public void InsertIntoGame(int xPosition, int yPosition, bool value)
    {
        table[xPosition, yPosition].currentValue = value;
    }

    public static double[,] ConvertToArray(Cell[,] array)
    {
        int height = array.GetLength(0);
        int width = array.GetLength(1);

        double[,] results = new double[height, width];

        Parallel.For(0, height, y =>
        {
            for (int x = 0; x < width; ++x)
            {
                results[y, x] = array[y, x].currentValue ? 1 : 0;
            }
        });

        return results;
    }
}