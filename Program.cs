using GameOfLife;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Bmp;
using SixLabors.ImageSharp.Formats.Gif;
using SixLabors.ImageSharp.PixelFormats;
using System.Drawing.Imaging;

int mapXSize = 60;
int mapYSize = 60;
int numbersOfRuns = 240;
int xMapResolution = 800;
int yMapResolution = 800;
int frameDelay = 10;

GenerateGif(mapXSize, mapYSize, numbersOfRuns, xMapResolution, yMapResolution, frameDelay);

void GenerateGif(int mapXSize, int mapYSize, int numbersOfRuns, int xMapResolution, int yMapResolution, int frameDelay)
{
    // Create empty image.
    Image<Rgba32> gif = new(xMapResolution, yMapResolution, Color.Black);

    // Set the delay between frames in (1/100) of a second.
    GifFrameMetadata metadata = gif.Frames.RootFrame.Metadata.GetGifMetadata();
    metadata.FrameDelay = frameDelay;

    var plt = new ScottPlot.Plot(xMapResolution, yMapResolution);
    var gameOfLifeEngine = new GameOfLifeEngine(mapXSize, mapYSize);

    // Initialize the Game of Life engine with a specific pattern.
    gameOfLifeEngine.InsertIntoGame(25, 45, true);
    gameOfLifeEngine.InsertIntoGame(25, 44, true);
    gameOfLifeEngine.InsertIntoGame(25, 46, true);
    gameOfLifeEngine.InsertIntoGame(23, 45, true);
    gameOfLifeEngine.InsertIntoGame(26, 45, true);

    var bmpDecoder = new BmpDecoder();
    // Create a memory stream and a bitmap to store the generated frames.
    var ms = new MemoryStream();
    var mapBIt = new System.Drawing.Bitmap(xMapResolution, yMapResolution);

    // Perform the specified number of runs.
    for (int k = 0; k < numbersOfRuns; k++)
    {
        // Calculate the next step in the simulation and convert it to a 2D array.
        var nextStep = gameOfLifeEngine.CalculateNext();
        var data2D = GameOfLifeEngine.ConvertToArray(nextStep);

        // Generate a heat map from the data and render it to the bitmap.
        plt.AddHeatmap(data2D);
        plt.Render(mapBIt, false, 1);

        // Save the bitmap as a BMP image in the memory stream.
        mapBIt.Save(ms, ImageFormat.Bmp);

        // Decode the BMP image from the memory stream and add it as a frame to the GIF.
        byte[] bitmapData = ms.ToArray();
        using var image = Image.Load(bitmapData, bmpDecoder);
        gif.Frames.AddFrame(image.Frames.RootFrame);
    }

    // Save the final result.
    gif.SaveAsGif("output.gif");
}
