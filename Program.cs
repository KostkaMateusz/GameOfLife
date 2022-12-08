using GameOfLife;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Bmp;
using SixLabors.ImageSharp.Formats.Gif;
using SixLabors.ImageSharp.PixelFormats;
using System.Drawing.Imaging;

int mapXSize = 60;
int mapYSize = 60;
int numbersOfRuns = 240;
int xMapResolution = 600;
int yMapResolution = 600;
int frameDelay = 10;

GenerateGif(mapXSize, mapYSize, numbersOfRuns, xMapResolution, yMapResolution, frameDelay);

/*ToDo:
 * Add Grid to HeatMap
 * Configure Labels in HeatMap
 * COnfigure Axes in HeatMap
 * Refactor Drawing Heat Map to new Clas
 */
void GenerateGif(int mapXSize, int mapYSize, int numbersOfRuns, int xMapResolution, int yMapResolution, int frameDelay)
{
    // Delay between frames in (1/100) of a second.
    // Create empty image.
    using Image<Rgba32> gif = new(xMapResolution, yMapResolution, Color.Black);

    // Set the delay until the next image is displayed.
    GifFrameMetadata metadata = gif.Frames.RootFrame.Metadata.GetGifMetadata();
    metadata.FrameDelay = frameDelay;

    var plt = new ScottPlot.Plot(xMapResolution, yMapResolution);
    var gameOfLifeEngine = new GameOfLifeEngine(mapXSize, mapYSize);

    gameOfLifeEngine.InsertIntoGame(25, 45, true);
    gameOfLifeEngine.InsertIntoGame(25, 44, true);
    gameOfLifeEngine.InsertIntoGame(25, 46, true);

    gameOfLifeEngine.InsertIntoGame(23, 45, true);
    gameOfLifeEngine.InsertIntoGame(26, 45, true);

    var ms = new MemoryStream();
    var mapBIt = new System.Drawing.Bitmap(xMapResolution, yMapResolution);

    Cell[,]? nextStep;
    double[,]? data2D;

    for (int k = 0; k < numbersOfRuns; k++)
    {
        nextStep = gameOfLifeEngine.CalculateNext();
        data2D = GameOfLifeEngine.ConvertToArray(nextStep);

        plt.AddHeatmap(data2D);
        plt.Render(mapBIt, true, 1);

        mapBIt.Save(ms, ImageFormat.Bmp);

        byte[] bitmapData = ms.ToArray();

        var bmpDecoder = new BmpDecoder();
        using var image = Image.Load(bitmapData, bmpDecoder);

        // Set the delay until the next image is displayed.
        metadata = image.Frames.RootFrame.Metadata.GetGifMetadata();
        metadata.FrameDelay = frameDelay;

        // Add the frame to the gif.
        gif.Frames.AddFrame(image.Frames.RootFrame);
    }
    // Save the final result.
    gif.SaveAsGif("output.gif");
}