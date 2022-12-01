namespace GameOfLife;


internal struct Point
{
    public int xPosition;
    public int yPosition;

    public Point(int xPosition, int yPosition)
    {
        this.xPosition = xPosition;
        this.yPosition = yPosition;
    }
}

class Cell
{
    public Point[] NeighborsAddresses = new Point[8];
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
            //if (NeighborsAddresses[i] != default(Point))
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

