
//Any live cell with two or three live neighbours survives.
//Any dead cell with three live neighbours becomes a live cell.
//All other live cells die in the next generation. Similarly, all other dead cells stay dead.

Cell[,] table = new Cell[10, 10];

// initalize objects
for (int i = 0; i < table.GetLength(0); i++)
    for (int j = 0; j < table.GetLength(1); j++)
        table[i, j] = new Cell(i, j, table.GetLength(0) - 1, table.GetLength(1) - 1);

for (int i = 0; i < table.GetLength(0); i++)
    for (int j = 0; j < table.GetLength(1); j++)
        table[i, j].SetNeighbors(ref table);


table[3, 4].currentValue = true;
table[3, 5].currentValue = true;
table[3, 6].currentValue = true;



for (int k = 0; k < 5; k++)
{
    for (int i = 0; i < table.GetLength(0); i++)
    {
        for (int j = 0; j < table.GetLength(1); j++)
        {
            Console.Write(table[i, j].currentValue.ToString() + " ");
        }
        Console.WriteLine();
    }
    Console.WriteLine();


    for (int i = 0; i < table.GetLength(0); i++)
    {
        for (int j = 0; j < table.GetLength(1); j++)
        {
            int number_of_live_cells = 0;
            foreach (var neighbour in table[i, j].Neighbors)
                if (neighbour != null && neighbour.currentValue == true)
                    number_of_live_cells++;

            if (table[i, j].currentValue == true && (number_of_live_cells == 2 || number_of_live_cells == 3))
            {
                table[i, j].futureValue = true;
            }
            else if (number_of_live_cells == 3)
            {
                table[i, j].futureValue = true;
            }
            else
            {
                table[i, j].futureValue = false;
            }
        }
    }

    for (int i = 0; i < table.GetLength(0); i++)
    {
        for (int j = 0; j < table.GetLength(1); j++)
        {
            table[i, j].Prepare();
        }

    }
}

Console.WriteLine(table);


class Addres
{
    public int xPosition;
    public int yPosition;

    public Addres(int xPosition, int yPosition)
    {
        this.xPosition = xPosition;
        this.yPosition = yPosition;
    }
}

class Cell
{
    public Addres[] NeighborsAddresses = new Addres[8];
    public bool currentValue = false;
    public bool futureValue = false;

    public Cell[] Neighbors = new Cell[8];

    private void CalculateNeighborsAddresses(int xPosition, int yPosition, int xSize, int ySize)
    {
        int xPositionDecremented = xPosition - 1;
        int yPositionDecremented = yPosition - 1;
        int xPositionIncremented = xPosition + 1;
        int yPositionIncremented = yPosition + 1;

        if (xPositionDecremented >= 0)
        {
            NeighborsAddresses[7] = new(xPositionDecremented, yPosition);

            if (yPositionDecremented >= 0)
                NeighborsAddresses[0] = new(xPositionDecremented, yPositionDecremented);

            if (yPositionIncremented < ySize)
                NeighborsAddresses[6] = new(xPositionDecremented, yPositionIncremented);
        }

        if (xPositionIncremented < xSize)
        {
            NeighborsAddresses[3] = new(xPositionIncremented, yPosition);

            if (yPositionDecremented >= 0)
                NeighborsAddresses[2] = new(xPositionIncremented, yPositionDecremented);

            if (yPositionIncremented < ySize)
                NeighborsAddresses[4] = new(xPositionIncremented, yPositionIncremented);
        }

        if (yPositionDecremented >= 0)
            NeighborsAddresses[1] = new(xPosition, yPositionDecremented);

        if (yPositionIncremented < ySize)
            NeighborsAddresses[5] = new(xPosition, yPositionIncremented);

    }

    public Cell(int xPosition, int yPosition, int xSize, int ySize)
    {
        CalculateNeighborsAddresses(xPosition, yPosition, xSize, ySize);
    }

    public void SetNeighbors(ref Cell[,] table)
    {
        for (int i = 0; i < NeighborsAddresses.Length; i++)
        {
            if (NeighborsAddresses[i] is not null)
            {
                Neighbors[i] = table[NeighborsAddresses[i].xPosition, NeighborsAddresses[i].yPosition];
            }
        }
    }

    public void Prepare()
    {
        this.currentValue = this.futureValue;
    }

}


