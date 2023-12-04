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

    public readonly int xPosition;
    public readonly int yPosition;
    public Cell(int xPosition, int yPosition, int xSize, int ySize)
    {
        this.xPosition = xPosition;
        this.yPosition=yPosition;

        CalculateNeighborsAddresses(xPosition, yPosition, xSize, ySize);
    }

    private void CalculateNeighborsAddresses(int xPosition, int yPosition, int xSize, int ySize)
    {
        int yPositionDecremented = yPosition - 1;
        int xPositionIncremented = xPosition + 1;
        int xPositionDecremented = xPosition - 1;
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

    public void Prepare()
    {
        currentValue = futureValue;
    }

}

