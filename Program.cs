

//Any live cell with two or three live neighbours survives.
//Any dead cell with three live neighbours becomes a live cell.
//All other live cells die in the next generation. Similarly, all other dead cells stay dead.



bool[,] table = new bool[37, 12];

bool[,] futureTable = new bool[37, 12];

class Addres
{
    public int? xPosition;
    public int? yPosition;

    public Addres(int? xPosition, int? yPosition)
    {
        this.xPosition = xPosition;
        this.yPosition = yPosition;
    }
}

class Cell
{
    public Addres? leftUpCornerNeighbour = null;
    public Addres? upNeighbour = null;
    public Addres? rightCornerNeighbour = null;
    public Addres? rightNeighbour = null;
    public Addres? rightDownCornerNeighbour = null;
    public Addres? downNeighbour = null;
    public Addres? leftDownCornerNeighbour = null;
    public Addres? leftNeighbour = null;

    public Cell(int xPosition, int yPosition, int xSize, int ySize)
    {
        int? xPositionDecremented = xPosition - 1 >= 0 ? xPosition - 1 : null;
        int? yPositionDecremented = yPosition - 1 >= 0 ? yPosition - 1 : null;
        int? xPositionIncremented = xPosition + 1 < xSize ? xPosition + 1 : null;
        int? yPositionIncremented = yPosition + 1 < ySize ? ySize + 1 : null;

        if (xPositionDecremented is not null)
        {
            leftNeighbour = new(xPositionDecremented, yPosition);

            if (yPositionDecremented is not null)
                leftUpCornerNeighbour = new(xPositionDecremented, yPositionDecremented);

            if (yPositionIncremented is not null)
                leftDownCornerNeighbour = new(xPositionDecremented, yPositionIncremented);
        }

        if (xPositionIncremented is not null)
        {
            rightNeighbour = new(xPositionIncremented, yPosition);

            if (yPositionDecremented is not null)
                rightCornerNeighbour = new(xPositionIncremented, yPositionDecremented);

            if (yPositionIncremented is not null)
                rightDownCornerNeighbour = new(xPositionIncremented, yPositionIncremented);
        }

        if (yPositionDecremented is not null)
            upNeighbour = new(xPosition, yPositionDecremented);

        if (yPositionIncremented is not null)
            downNeighbour = new(xPosition, yPositionIncremented);

    }
}


